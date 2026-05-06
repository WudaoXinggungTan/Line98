using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.SceneLoaderService;
using GameCoreScripts.Services.GameStateService;
using GameCoreScripts._GameplayEnterData;

namespace GameCoreScripts._GameplayState
{
    public class GameplayState : BaseGameState<GameplayInitiatorEnterData>
    {
        public override GameStateType GameStateType => GameStateType.Gameplay;

        public GameplayState(GameplayInitiatorEnterData gamePlayStateEnterData, ISceneLoaderService sceneLoaderService) : base(gamePlayStateEnterData, sceneLoaderService)
        {
        }

        public override async Awaitable LoadState(CancellationTokenSource cancellationTokenSource)
        {
            await base.LoadState(cancellationTokenSource);
            await SceneLoaderService.TryLoadScene(SceneType.GameplayScene, EnterData, cancellationTokenSource);
        }

        public override async Awaitable StartState(CancellationTokenSource cancellationTokenSource)
        {
            await base.StartState(cancellationTokenSource);
            await SceneLoaderService.StartScene(SceneType.GameplayScene, EnterData, cancellationTokenSource);
        }

        public override async Awaitable ExitState(CancellationTokenSource cancellationTokenSource)
        {
            await base.ExitState(cancellationTokenSource);
            await SceneLoaderService.TryUnloadScene(SceneType.GameplayScene, cancellationTokenSource);
        }
    }
}