using System.Collections.Generic;
using CompanyCoreScripts.MVC.UICamera;
using GameplayScripts.MVC.Gameplay.Ball;
using GameplayScripts.Services.ScoreService;

namespace GameplayScripts.MVC.UI.HUD
{
    public class HudUICanvasController : IHudUICanvasController
    {
        #region Dependencies

        private readonly IUICameraController uiCameraController;
        private readonly IGameplayUIGroupController gameplayUIGroupController;
        private readonly HudUICanvasView hudUICanvasView;
        private readonly IBallController ballController;
        private readonly IScoreService scoreService;

        #endregion

        #region Constructor

        public HudUICanvasController(IUICameraController uiCameraController, IGameplayUIGroupController gameplayUIGroupController, HudUICanvasView hudUICanvasView,
            IBallController ballController, IScoreService scoreService)
        {
            this.uiCameraController = uiCameraController;
            this.gameplayUIGroupController = gameplayUIGroupController;
            this.hudUICanvasView = hudUICanvasView;
            this.ballController = ballController;
            this.scoreService = scoreService;
        }

        #endregion

        #region Public Methods

        public void InitEntryPoint()
        {
            hudUICanvasView.InitEntryPoint(uiCameraController.UICamera, OnRestartButtonClicked, OnSettingsButtonClicked, OnUndoButtonClicked, OnNewGameButtonClicked);

            ballController.NextBallsChanged += OnNextBallsChanged;
            ballController.UndoAvailabilityChanged += OnUndoAvailabilityChanged;
            scoreService.ScoreChanged += OnScoreChanged;

            OnScoreChanged();
            hudUICanvasView.SetUndoInteractable(ballController.CanUndo);
        }

        public void StartEntryPoint()
        {
            hudUICanvasView.StartEntryPoint();
        }

        public void InitExitPoint()
        {
            ballController.NextBallsChanged -= OnNextBallsChanged;
            ballController.UndoAvailabilityChanged -= OnUndoAvailabilityChanged;
            scoreService.ScoreChanged -= OnScoreChanged;

            hudUICanvasView.InitExitPoint();
        }

        #endregion

        #region Private Methods

        private void OnRestartButtonClicked()
        {
            ballController.Restart();
        }

        // Settings opens as a panel over the HUD; sound and other options live there
        private void OnSettingsButtonClicked()
        {
            gameplayUIGroupController.OpenSettingsCanvas();
        }

        private void OnUndoButtonClicked()
        {
            ballController.Undo();
        }

        private void OnNewGameButtonClicked()
        {
            ballController.NewGame();
        }

        private void OnNextBallsChanged(IReadOnlyList<BallColorType> colors)
        {
            hudUICanvasView.ShowNextBalls(colors);
        }

        private void OnUndoAvailabilityChanged(bool canUndo)
        {
            hudUICanvasView.SetUndoInteractable(canUndo);
        }

        private void OnScoreChanged()
        {
            hudUICanvasView.SetScore(scoreService.Score);
            hudUICanvasView.SetBestScore(scoreService.BestScore);
        }

        #endregion
    }
}
