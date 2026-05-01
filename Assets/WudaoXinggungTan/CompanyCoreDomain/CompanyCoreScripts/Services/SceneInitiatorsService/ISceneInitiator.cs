using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.SceneLoaderService;

namespace CompanyCoreScripts.Services.SceneInitiatorsService
{
    public interface ISceneInitiator
    {
        SceneType SceneType { get; }
        Awaitable LoadEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource);
        Awaitable StartEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource);
        Awaitable InitExitPoint(CancellationTokenSource cancellationTokenSource);
    }
}
