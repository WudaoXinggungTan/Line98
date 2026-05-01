using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.SceneLoaderService;

namespace CompanyCoreScripts.Services.SceneInitiatorsService
{
    public interface ISceneInitiatorsService
    {
        void RegisterInitiator(ISceneInitiator sceneInitiator);
        void UnregisterInitiator(ISceneInitiator sceneInitiator);
        Awaitable InvokeInitiatorLoadEntryPoint(SceneType sceneType, IInitiatorEnterData enterData, CancellationTokenSource cancellationTokenSource);
        Awaitable InvokeInitiatorStartEntryPoint(SceneType sceneType, IInitiatorEnterData enterData, CancellationTokenSource cancellationTokenSource);
        Awaitable InvokeInitiatorExitPoint(SceneType sceneType, CancellationTokenSource cancellationTokenSource);
    }
}
