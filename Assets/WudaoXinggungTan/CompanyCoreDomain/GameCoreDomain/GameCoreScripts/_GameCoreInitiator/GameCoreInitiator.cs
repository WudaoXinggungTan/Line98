using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.SceneInitiatorsService;
using CompanyCoreScripts.Services.SceneLoaderService;
using CompanyCoreScripts.Utils;
using CompanyCoreScripts._GameCoreEnterData;
using GameCoreScripts.Services.GameStateService;
using GameCoreScripts.MVC.LoadingScreen;
using GameCoreScripts._GameplayEnterData;
using GameCoreScripts._GameplayState;
using GameCoreScripts._MainMenuEnterData;
using GameCoreScripts._MainMenuState;

namespace GameCoreScripts._GameCoreInitiator
{
    public class GameCoreInitiator : ISceneInitiator, IGameCoreInitiator
    {
        public SceneType SceneType => SceneType.GameCoreScene;

        private readonly ISceneInitiatorsService sceneInitiatorsService;
        private readonly IGameStateService gameStateService;
        private readonly ILoadingScreenController loadingScreenController;

        private readonly MainMenuStateFactory mainMenuStateFactory;
        private readonly GameplayStateFactory gameplayStateFactory; // For future uses


        public GameCoreInitiator(ISceneInitiatorsService sceneInitiatorsService, IGameStateService gameStateService, ILoadingScreenController loadingScreenController, MainMenuStateFactory mainMenuStateFactory, GameplayStateFactory gameplayStateFactory)
        {
            this.sceneInitiatorsService = sceneInitiatorsService;
            this.sceneInitiatorsService.RegisterInitiator(this);
            this.gameStateService = gameStateService;
            this.loadingScreenController = loadingScreenController;

            this.mainMenuStateFactory = mainMenuStateFactory;
            this.gameplayStateFactory = gameplayStateFactory;
        }

        public Awaitable LoadEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource)
        {
            var enterData = (GameCoreInitiatorEnterData)enterDataObject; // kept for future use
            
            loadingScreenController.ResetSlider();
            return AwaitableUtils.CompletedTask;
        }

        public async Awaitable StartEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource)
        {
            var enterData = (GameCoreInitiatorEnterData)enterDataObject; // kept for future use
            
            await loadingScreenController.Show(cancellationTokenSource);
            await loadingScreenController.SetLoadingSlider(0.5f, cancellationTokenSource);
            
            await gameStateService.EnterInitialGameState(mainMenuStateFactory.Create(new MainMenuInitiatorEnterData()), cancellationTokenSource);
            
            await loadingScreenController.SetLoadingSlider(1f, cancellationTokenSource);
            loadingScreenController.Hide();
        }

        public Awaitable InitExitPoint(CancellationTokenSource cancellationTokenSource)
        {
            sceneInitiatorsService.UnregisterInitiator(this);
            return AwaitableUtils.CompletedTask;
        }
    }
}