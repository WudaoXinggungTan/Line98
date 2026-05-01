using System.Threading;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using CompanyCoreScripts.Services.SceneInitiatorsService;
using CompanyCoreScripts.Services.SceneLoaderService;
using CompanyCoreScripts.Utils;
using UnityEngine;

namespace GameplayScripts._GameplayInitiator
{
    public class GameplayInitiator : ISceneInitiator, IGameplayInitiator
    {
        public SceneType SceneType => SceneType.GameplayScene;

        private readonly ISceneInitiatorsService sceneInitiatorsService;

        public GameplayInitiator(ISceneInitiatorsService sceneInitiatorsService)
        {
            this.sceneInitiatorsService = sceneInitiatorsService;
            this.sceneInitiatorsService.RegisterInitiator(this);
        }

        public async Awaitable LoadEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource)
        {
            MyLoggerService.Log("LoadEntryPoint");
        }

        public Awaitable StartEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource)
        {
            return AwaitableUtils.CompletedTask;
        }

        public Awaitable InitExitPoint(CancellationTokenSource cancellationTokenSource)
        {
            return AwaitableUtils.CompletedTask;
        }
    }
}