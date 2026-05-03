using UnityEngine;
using CompanyCoreScripts.MVC.UICamera;
using CompanyCoreScripts.Services.GameStateService;
using GameCoreScripts._GameplayEnterData;
using GameCoreScripts._GameplayState;

namespace MainMenuScripts.MVC.UI.MainMenu
{
    public class MainMenuUICanvasController : IMainMenuUICanvasController
    {
        private readonly IGameStateService gameStateService;
        private readonly IUICameraController uiCameraController;
        private readonly MainMenuUICanvasView mainMenuUICanvasView;
        private readonly GameplayStateFactory gameplayStateFactory;

        public MainMenuUICanvasController(IGameStateService gameStateService, IUICameraController uiCameraController, MainMenuUICanvasView mainMenuUICanvasView, GameplayStateFactory gameplayStateFactory)
        {
            this.gameStateService = gameStateService;
            this.uiCameraController = uiCameraController;
            this.mainMenuUICanvasView = mainMenuUICanvasView;
            this.gameplayStateFactory = gameplayStateFactory;
        }

        public void InitEntryPoint()
        {
            mainMenuUICanvasView.InitEntryPoint(uiCameraController.UICamera, OnStartButtonClicked, OnQuitButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            gameStateService.SwitchState(gameplayStateFactory.Create(new GameplayInitiatorEnterData()));
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }

        public void ExitEntryPoint()
        {
            mainMenuUICanvasView.ExitEntryPoint();
        }
    }
}