using UnityEngine;
using CompanyCoreScripts.MVC.UICamera;
using GameCoreScripts.Services.GameStateService;
using GameCoreScripts._GameplayEnterData;
using GameCoreScripts._GameplayState;

namespace MainMenuScripts.MVC.UI.MainCanvas
{
    public class MainUICanvasController : IMainUICanvasController
    {
        #region Dependencies

        private readonly IGameStateService gameStateService;
        private readonly GameplayStateFactory gameplayStateFactory;
        private readonly IUICameraController uiCameraController;
        private readonly IMainMenuUIGroupController mainMenuUIGroupController;
        private readonly MainUICanvasView mainUICanvasView;


        #endregion

        #region Constructor

        public MainUICanvasController(IGameStateService gameStateService, GameplayStateFactory gameplayStateFactory, 
            IUICameraController uiCameraController, IMainMenuUIGroupController mainMenuUIGroupController, MainUICanvasView mainUICanvasView)
        {
            this.gameStateService = gameStateService;
            this.gameplayStateFactory = gameplayStateFactory;
            this.uiCameraController = uiCameraController;
            this.mainMenuUIGroupController = mainMenuUIGroupController;
            this.mainUICanvasView = mainUICanvasView;
        }

        #endregion

        #region Public Methods

        public void InitEntryPoint()
        {
            mainUICanvasView.InitEntryPoint(uiCameraController.UICamera, OnStartButtonClicked, OnSettingsButtonClicked, OnQuitButtonClicked);
        }

        public void StartEntryPoint()
        {
            mainUICanvasView.StartEntryPoint();
        }

        public void InitExitPoint()
        {
            mainUICanvasView.InitExitPoint();
        }

        #endregion

        #region Private Methods

        private void OnStartButtonClicked()
        {
            gameStateService.SwitchState(gameplayStateFactory.Create(new GameplayInitiatorEnterData()));
        }

        private void OnSettingsButtonClicked()
        {
            mainMenuUIGroupController.OpenSettingsCanvas();
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }

        #endregion
    }
}