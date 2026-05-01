using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.GameStateService;
using CompanyCoreScripts.Services.SceneInitiatorsService;
using CompanyCoreScripts.Services.SceneLoaderService;
using CompanyCoreScripts.Utils;
using CompanyCoreScripts._GameCoreEnterData;
using GameCoreScripts._GameplayEnterData;
using GameCoreScripts._GameplayState;
using GameCoreScripts._MainMenuEnterData;
using GameCoreScripts._MainMenuState;

namespace GameCoreScripts._GameCoreInitiator
{
    public class GameCoreSceneInitiator : ISceneInitiator, IGameCoreInitiator
    {
        public SceneType SceneType => SceneType.GameCoreScene;

        private readonly ISceneInitiatorsService sceneInitiatorsService;
        private readonly IGameStateService gameStateService;
        
        private readonly MainMenuStateFactory mainMenuStateFactory;
        private readonly GameplayStateFactory gameplayStateFactory; // For future uses

        public GameCoreSceneInitiator(ISceneInitiatorsService sceneInitiatorsService, IGameStateService gameStateService, 
            MainMenuStateFactory mainMenuStateFactory, GameplayStateFactory gameplayStateFactory)
        {
            this.sceneInitiatorsService = sceneInitiatorsService;
            this.sceneInitiatorsService.RegisterInitiator(this);
            this.gameStateService = gameStateService;
            this.mainMenuStateFactory = mainMenuStateFactory;
            this.gameplayStateFactory = gameplayStateFactory;
        }

        public async Awaitable LoadEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource)
        {
            var enterData = (GameCoreInitiatorEnterData)enterDataObject; // kept for future use
            await gameStateService.EnterInitialGameState(mainMenuStateFactory.Create(new MainMenuInitiatorEnterData()), cancellationTokenSource);
        }

        public Awaitable StartEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource)
        {
            var enterData = (GameCoreInitiatorEnterData)enterDataObject; // kept for future use
            return AwaitableUtils.CompletedTask;
        }

        public Awaitable InitExitPoint(CancellationTokenSource cancellationTokenSource)
        {
            sceneInitiatorsService.UnregisterInitiator(this);
            return AwaitableUtils.CompletedTask;
        }
    }
}
