using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.LoggerService;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using CompanyCoreScripts.Services.SceneInitiatorsService;
using CompanyCoreScripts.Services.SceneLoaderService;
using CompanyCoreScripts.Utils;

namespace GameCoreScripts.Services.GameStateService
{
    public abstract class BaseGameState<T> : IGameState where T : class, IInitiatorEnterData
    {
        private readonly CancellationTokenSource cancellationTokenSource;
        protected readonly ISceneLoaderService SceneLoaderService;
        protected T EnterData { get; }
        
        protected BaseGameState(T enterData, ISceneLoaderService sceneLoaderService)
        {
            EnterData = enterData;
            SceneLoaderService = sceneLoaderService;
            cancellationTokenSource = new CancellationTokenSource();
        }

        public CancellationTokenSource CancellationTokenSource => CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenSource.Token);
        public abstract GameStateType GameStateType { get; }

        public virtual Awaitable LoadState(CancellationTokenSource cancellationTokenSource)
        {
            MyLoggerService.LogTopic($"Load state {GameStateType}", LogTopicType.GameState);
            return AwaitableUtils.CompletedTask;
        }
        
        public virtual Awaitable StartState(CancellationTokenSource cancellationTokenSource)
        {
            MyLoggerService.LogTopic($"Start state {GameStateType}", LogTopicType.GameState);
            return AwaitableUtils.CompletedTask;
        }

        public virtual Awaitable ExitState(CancellationTokenSource cancellationTokenSource)
        {
            this.cancellationTokenSource.Cancel();
            return AwaitableUtils.CompletedTask;
        }
    }
}
