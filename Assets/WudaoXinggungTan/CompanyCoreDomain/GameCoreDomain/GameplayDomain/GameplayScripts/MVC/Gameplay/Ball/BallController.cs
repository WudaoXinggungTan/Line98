using System;
using System.Collections.Generic;
using UnityEngine;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using GameplayScripts.MVC.Gameplay.Board;

namespace GameplayScripts.MVC.Gameplay.Ball
{
    public class BallController : IBallController
    {
        #region Constants

        private const int BALLS_TO_SPAWN = 3;

        #endregion

        #region Dependencies

        private readonly IBoardController boardController;
        private readonly BallPool ballPool;

        private readonly Dictionary<Vector2Int, (BallModel model, BallView view)> balls = new();
        private readonly int colorCount = Enum.GetValues(typeof(BallColorType)).Length - 1; // without None
        private Vector2Int? selectedCell;

        #endregion

        #region Constructor

        public BallController(IBoardController boardController, BallPool ballPool)
        {
            this.boardController = boardController;
            this.ballPool = ballPool;
        }

        #endregion

        #region Public Methods

        public void InitEntryPoint()
        {
            ballPool.InitPool();
            boardController.CellClicked += OnCellClicked;
        }

        public void StartEntryPoint()
        {
            SpawnRandomBalls(BALLS_TO_SPAWN);
        }

        public void InitExitPoint()
        {
            boardController.CellClicked -= OnCellClicked;
            Deselect();

            foreach (var (model, view) in balls.Values)
            {
                boardController.BoardModel.Set(model.Cell, (int)BallColorType.None);
                view.Release();
            }

            balls.Clear();
        }

        #endregion

        #region Private Methods

        private void SpawnRandomBalls(int count)
        {
            var emptyCells = boardController.BoardModel.GetEmptyCells();
            var spawnCount = Mathf.Min(count, emptyCells.Count);
            if (spawnCount < count)
            {
                MyLoggerService.Log($"Only {emptyCells.Count} empty cells left, spawning {spawnCount} of {count} balls");
            }

            for (var i = 0; i < spawnCount; i++)
            {
                var index = UnityEngine.Random.Range(0, emptyCells.Count);
                var cell = emptyCells[index];
                emptyCells.RemoveAt(index);

                var color = (BallColorType)UnityEngine.Random.Range(1, colorCount + 1);
                SpawnBall(color, cell);
            }
        }

        private void SpawnBall(BallColorType color, Vector2Int cell)
        {
            var model = new BallModel(color, cell);
            boardController.BoardModel.Set(cell, (int)color);

            var view = ballPool.Spawn();
            view.Show(color, boardController.GetCellTransform(cell));

            balls[cell] = (model, view);
        }

        private void OnCellClicked(Vector2Int cell)
        {
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

        // No path check yet: a selected ball can jump to any empty cell
        private void MoveBall(Vector2Int from, Vector2Int to)
        {
            var (model, view) = balls[from];
            Deselect();

            balls.Remove(from);
            boardController.BoardModel.Set(from, (int)BallColorType.None);

            boardController.BoardModel.Set(to, (int)model.Color);
            view.Show(model.Color, boardController.GetCellTransform(to));
            balls[to] = (new BallModel(model.Color, to), view);
        }

        #endregion
    }
}
