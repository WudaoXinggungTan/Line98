using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.GameStateService;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using CompanyCoreScripts.Services.SceneInitiatorsService;
using CompanyCoreScripts.Services.SceneLoaderService;
using CompanyCoreScripts.Utils;
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
        private readonly MainMenuInitiatorEnterData mainMenuInitiatorEnterData;        
        private readonly GameplayStateFactory gameplayStateFactory; // For future uses
        private readonly GameplayInitiatorEnterData gameplayInitiatorEnterData; // For future uses

        public GameCoreSceneInitiator(ISceneInitiatorsService sceneInitiatorsService, IGameStateService gameStateService, 
            MainMenuStateFactory mainMenuStateFactory, MainMenuInitiatorEnterData mainMenuInitiatorEnterData,
            GameplayStateFactory gameplayStateFactory, GameplayInitiatorEnterData gameplayInitiatorEnterData)
        {
            this.sceneInitiatorsService = sceneInitiatorsService;
            this.sceneInitiatorsService.RegisterInitiator(this);
            this.gameStateService = gameStateService;
            this.mainMenuStateFactory = mainMenuStateFactory;
            this.mainMenuInitiatorEnterData = mainMenuInitiatorEnterData;
            this.gameplayStateFactory = gameplayStateFactory;
            this.gameplayInitiatorEnterData = gameplayInitiatorEnterData;
        }

        public async Awaitable LoadEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource)
        {
            await gameStateService.EnterInitialGameState(mainMenuStateFactory.Create(mainMenuInitiatorEnterData), cancellationTokenSource);
        }

        public Awaitable StartEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource)
        {
            return AwaitableUtils.CompletedTask;
        }

        public Awaitable InitExitPoint(CancellationTokenSource cancellationTokenSource)
        {
            sceneInitiatorsService.UnregisterInitiator(this);
            return AwaitableUtils.CompletedTask;
        }
    }
}
