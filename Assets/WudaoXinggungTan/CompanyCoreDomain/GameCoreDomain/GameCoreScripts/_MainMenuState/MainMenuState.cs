using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.SceneLoaderService;
using GameCoreScripts.Services.GameStateService;
using GameCoreScripts._MainMenuEnterData;

namespace GameCoreScripts._MainMenuState
{
    public class MainMenuState : BaseGameState<MainMenuInitiatorEnterData>
    {
        public override GameStateType GameStateType => GameStateType.MainMenu;

        public MainMenuState(MainMenuInitiatorEnterData mainMenuInitiatorEnterData, ISceneLoaderService sceneLoaderService) : base(mainMenuInitiatorEnterData, sceneLoaderService)
        {
            Debug.Log($"THIS IS SIXTY-NINE!! - {mainMenuInitiatorEnterData.TestNumber}");
        }

        public override async Awaitable LoadState(CancellationTokenSource cancellationTokenSource)
        {
            await base.LoadState(cancellationTokenSource);
            await SceneLoaderService.TryLoadScene(SceneType.MainMenuScene, EnterData, cancellationTokenSource);
        }

        public override async Awaitable StartState(CancellationTokenSource cancellationTokenSource)
        {
            await base.StartState(cancellationTokenSource);
            await SceneLoaderService.StartScene(SceneType.MainMenuScene, EnterData, cancellationTokenSource);
        }

        public override async Awaitable ExitState(CancellationTokenSource cancellationTokenSource)
        {
            await base.ExitState(cancellationTokenSource);
            await SceneLoaderService.TryUnloadScene(SceneType.MainMenuScene, cancellationTokenSource);
        }
    }
}