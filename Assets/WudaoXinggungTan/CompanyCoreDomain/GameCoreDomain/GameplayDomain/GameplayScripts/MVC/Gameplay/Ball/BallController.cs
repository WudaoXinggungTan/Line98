using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.CommandFactoryService.Factory;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using GameplayScripts.Commands.GameOver;
using GameplayScripts.MVC.Gameplay.Board;
using GameplayScripts.Services.ScoreService;

namespace GameplayScripts.MVC.Gameplay.Ball
{
    // Turn flow: a new game deals STARTING_BALLS balls and BALLS_PER_TURN previews (next turn's balls, drawn small).
    // After every move the previews grow into balls and new previews appear in empty cells,
    // unless the moved ball completes a line of 5+ (BallLineFinder): the line is cleared for points and the turn is free.
    // A preview covered by a moved ball is dropped. The game is lost when fewer than BALLS_PER_TURN cells are left for the next previews.
    // BoardModel stores a ball as +color and a preview as -color, so copies of it are full save states:
    // startBoard (for Restart) and undoBoard (for one-step Undo).
    public class BallController : IBallController
    {
        #region Constants

        private const int STARTING_BALLS = 6;
        private const int BALLS_PER_TURN = 3;
        private const float GAME_OVER_DELAY_SECONDS = 1f;

        #endregion

        #region Dependencies

        private readonly IBoardController boardController;
        private readonly BallPool ballPool;
        private readonly ICommandFactory commandFactory;
        private readonly IScoreService scoreService;

        private readonly Dictionary<Vector2Int, (BallModel model, BallView view)> balls = new();
        private readonly Dictionary<Vector2Int, (BallModel model, BallView view)> previews = new();
        private readonly int colorCount = Enum.GetValues(typeof(BallColorType)).Length - 1; // without None
        private Vector2Int? selectedCell;
        private bool isMoving;
        private CancellationTokenSource moveCancellationTokenSource;

        private BoardModel startBoard;
        private BoardModel undoBoard;
        private int undoScore;

        #endregion

        #region Properties

        public bool CanUndo => undoBoard != null;

        public event Action<IReadOnlyList<BallColorType>> NextBallsChanged;
        public event Action<bool> UndoAvailabilityChanged;

        #endregion

        #region Constructor

        public BallController(IBoardController boardController, BallPool ballPool, ICommandFactory commandFactory, IScoreService scoreService)
        {
            this.boardController = boardController;
            this.ballPool = ballPool;
            this.commandFactory = commandFactory;
            this.scoreService = scoreService;
        }

        #endregion

        #region Public Methods

        public void InitEntryPoint()
        {
            ballPool.InitPool();
            moveCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(Application.exitCancellationToken);
            boardController.CellClicked += OnCellClicked;
        }

        public void StartEntryPoint()
        {
            NewGame();
        }

        public void InitExitPoint()
        {
            boardController.CellClicked -= OnCellClicked;
            moveCancellationTokenSource?.Cancel();
            moveCancellationTokenSource?.Dispose();
            moveCancellationTokenSource = null;
            isMoving = false;
            Deselect();

            ReleaseAll(balls);
            ReleaseAll(previews);
            startBoard = null;
            undoBoard = null;
        }

        // A brand-new random board
        public void NewGame()
        {
            if (isMoving)
            {
                return;
            }

            Deselect();
            ReleaseAll(balls);
            ReleaseAll(previews);
            boardController.BoardModel.Clear();

            SpawnRandomBalls(STARTING_BALLS, false);
            SpawnRandomBalls(BALLS_PER_TURN, true);
            scoreService.ResetScore();

            startBoard = boardController.BoardModel.Clone();
            ClearUndo();
            NotifyNextBallsChanged();
        }

        // The current game again, from the board it was dealt with
        public void Restart()
        {
            if (isMoving || startBoard == null)
            {
                return;
            }

            RestoreBoard(startBoard, 0);
            ClearUndo();
        }

        // One step back: the board and score from just before the last move
        public void Undo()
        {
            if (isMoving || undoBoard == null)
            {
                return;
            }

            RestoreBoard(undoBoard, undoScore);
            ClearUndo();
        }

        #endregion

        #region Private Methods

        // Picks random cells that hold neither a ball nor a preview
        private void SpawnRandomBalls(int count, bool asPreview)
        {
            var emptyCells = boardController.BoardModel.GetEmptyCells();
            var spawnCount = Mathf.Min(count, emptyCells.Count);
            if (spawnCount < count)
            {
                MyLoggerService.Log($"Only {emptyCells.Count} empty cells left, spawning {spawnCount} of {count} {(asPreview ? "previews" : "balls")}");
            }

            for (var i = 0; i < spawnCount; i++)
            {
                var index = UnityEngine.Random.Range(0, emptyCells.Count);
                var cell = emptyCells[index];
                emptyCells.RemoveAt(index);

                var color = (BallColorType)UnityEngine.Random.Range(1, colorCount + 1);
                SpawnBall(color, cell, asPreview);
            }
        }

        private void SpawnBall(BallColorType color, Vector2Int cell, bool asPreview)
        {
            boardController.BoardModel.Set(cell, asPreview ? -(int)color : (int)color);
            CreateView(color, cell, asPreview);
        }

        private void CreateView(BallColorType color, Vector2Int cell, bool asPreview)
        {
            var view = ballPool.Spawn();
            view.Show(color, boardController.GetCellTransform(cell), asPreview);

            var targetCollection = asPreview ? previews : balls;
            targetCollection[cell] = (new BallModel(color, cell), view);
        }

        // Puts a saved board back: the live BoardModel is overwritten in place, then the views are rebuilt from it
        private void RestoreBoard(BoardModel savedBoard, int score)
        {
            Deselect();
            ReleaseAll(balls);
            ReleaseAll(previews);

            boardController.BoardModel.CopyFrom(savedBoard);
            RebuildViewsFromBoard();
            scoreService.SetScore(score);
            NotifyNextBallsChanged();
        }

        private void RebuildViewsFromBoard()
        {
            var board = boardController.BoardModel;
            for (var row = 0; row < board.Rows; row++)
            {
                for (var column = 0; column < board.Columns; column++)
                {
                    var cell = new Vector2Int(column, row);
                    var value = board.Get(cell);
                    if (value == 0)
                    {
                        continue;
                    }

                    CreateView((BallColorType)Mathf.Abs(value), cell, value < 0);
                }
            }
        }

        private void ClearUndo()
        {
            undoBoard = null;
            UndoAvailabilityChanged?.Invoke(false);
        }

        private void NotifyNextBallsChanged()
        {
            NextBallsChanged?.Invoke(previews.Values.Select(preview => preview.model.Color).ToList());
        }

        // Removes every ball in a line of 5+ through the given cells (returned to the pool) and awards points.
        // Returns how many balls were cleared.
        private int ClearLinesThrough(IEnumerable<Vector2Int> cells)
        {
            var cellsToClear = new HashSet<Vector2Int>();
            foreach (var cell in cells)
            {
                cellsToClear.UnionWith(BallLineFinder.FindLinesThrough(boardController.BoardModel, cell));
            }

            if (cellsToClear.Count == 0)
            {
                return 0;
            }

            foreach (var cell in cellsToClear)
            {
                var (_, view) = balls[cell];
                balls.Remove(cell);
                boardController.BoardModel.Set(cell, (int)BallColorType.None);
                view.Release();
            }

            var points = CalculatePoints(cellsToClear.Count);
            scoreService.AddPoints(points);
            MyLoggerService.Log($"Cleared {cellsToClear.Count} balls, +{points} points");
            return cellsToClear.Count;
        }

        // Classic Line98 scoring: 5 balls = 5 points, every extra ball +2 (6 = 7, 7 = 9, ...)
        private static int CalculatePoints(int clearedBalls)
        {
            return 2 * clearedBalls - 5;
        }

        // Previews become real balls where they stand
        private void GrowPreviews()
        {
            foreach (var pair in previews)
            {
                var (model, view) = pair.Value;
                boardController.BoardModel.Set(pair.Key, (int)model.Color);
                view.GrowToFullSize();
                balls[pair.Key] = (model, view);
            }

            previews.Clear();
        }

        // End of a turn: previews grow into balls (clearing any line they complete), then new previews are dealt.
        // Repeats while there is no ball on the board, so the player always has something to move.
        // Returns false when fewer than BALLS_PER_TURN empty cells are left for the new previews: the game is lost.
        private bool TryAdvanceTurn()
        {
            do
            {
                var grownCells = previews.Keys.ToList();
                GrowPreviews();
                ClearLinesThrough(grownCells);

                if (boardController.BoardModel.GetEmptyCells().Count < BALLS_PER_TURN)
                {
                    return false;
                }

                SpawnRandomBalls(BALLS_PER_TURN, true);
            }
            while (balls.Count == 0);

            return true;
        }

        private void ReleaseAll(Dictionary<Vector2Int, (BallModel model, BallView view)> collection)
        {
            foreach (var (model, view) in collection.Values)
            {
                boardController.BoardModel.Set(model.Cell, (int)BallColorType.None);
                view.Release();
            }

            collection.Clear();
        }

        private void OnCellClicked(Vector2Int cell)
        {
            // Ignore clicks until the moving ball arrives, so a second move can't start mid-animation
            if (isMoving)
            {
                return;
            }

            if (balls.ContainsKey(cell))
            {
                // Clicking the selected ball again deselects it, clicking another ball selects that one instead
                var wasSelected = selectedCell == cell;
                Deselect();
                if (!wasSelected)
                {
                    Select(cell);
                }

                return;
            }

            // Empty cells and preview cells are both valid targets (a preview that is landed on is dropped)
            if (selectedCell.HasValue)
            {
                MoveBall(selectedCell.Value, cell);
            }
        }

        private void Select(Vector2Int cell)
        {
            selectedCell = cell;
            balls[cell].view.SetSelected(true);
        }

        private void Deselect()
        {
            if (!selectedCell.HasValue)
            {
                return;
            }

            balls[selectedCell.Value].view.SetSelected(false);
            selectedCell = null;
        }

        // A ball moves only if BallPathFinder finds a way there through free cells; otherwise it shakes and stays selected
        private void MoveBall(Vector2Int from, Vector2Int to)
        {
            if (!BallPathFinder.TryFindPath(boardController.BoardModel, from, to, out var path))
            {
                MyLoggerService.Log($"No path from {from} to {to}, the ball stays where it is");
                balls[from].view.PlayBlockedFeedback();
                return;
            }

            _ = MoveBallAsync(from, to, path, moveCancellationTokenSource.Token);
        }

        private async Awaitable MoveBallAsync(Vector2Int from, Vector2Int to, List<Vector2Int> path, CancellationToken cancellationToken)
        {
            var (model, view) = balls[from];
            Deselect();

            // Saved before anything changes, so Undo can put this exact board back
            undoBoard = boardController.BoardModel.Clone();
            undoScore = scoreService.Score;

            var landsOnPreview = previews.Remove(to, out var displacedPreview);

            // The data changes right away, the view catches up while the ball walks the path
            balls.Remove(from);
            boardController.BoardModel.Set(from, (int)BallColorType.None);
            boardController.BoardModel.Set(to, (int)model.Color);
            balls[to] = (new BallModel(model.Color, to), view);

            // A preview covered by the moved ball is dropped: it doesn't grow this turn
            if (landsOnPreview)
            {
                displacedPreview.view.Release();
            }

            isMoving = true;
            try
            {
                var pathCells = path.ConvertAll(cell => boardController.GetCellTransform(cell));
                await view.MoveAlongPathAsync(pathCells, cancellationToken);
                view.Show(model.Color, boardController.GetCellTransform(to));

                // A line made by the moved ball is cleared and is a free turn: previews stay previews, nothing new spawns.
                // Exception: when the clear leaves no ball to move, the next turn starts by itself.
                if (ClearLinesThrough(new[] { to }) > 0 && balls.Count > 0)
                {
                    NotifyNextBallsChanged();
                    UndoAvailabilityChanged?.Invoke(true);
                    return;
                }

                if (!TryAdvanceTurn())
                {
                    // Not enough room for the next previews: input stays locked for a moment so the player sees the board,
                    // then the game is lost. GameOverCommand leaves gameplay, which runs InitExitPoint, so nothing may touch the balls after it.
                    await Awaitable.WaitForSecondsAsync(GAME_OVER_DELAY_SECONDS, cancellationToken);
                    commandFactory.CreateCommandVoid<GameOverCommand>().Execute();
                    return;
                }

                NotifyNextBallsChanged();
                UndoAvailabilityChanged?.Invoke(true);
            }
            catch (OperationCanceledException)
            {
                // Gameplay was left mid-move; InitExitPoint returns the balls to the pool
            }
            finally
            {
                isMoving = false;
            }
        }

        #endregion
    }
}
