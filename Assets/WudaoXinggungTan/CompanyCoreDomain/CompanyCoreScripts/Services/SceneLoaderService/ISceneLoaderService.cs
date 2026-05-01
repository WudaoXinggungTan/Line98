using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.SceneInitiatorsService;
using CompanyCoreScripts.MVC.LoadingScreen;

namespace CompanyCoreScripts.Services.SceneLoaderService
{
    public interface ISceneLoaderService
    {
        Awaitable<bool> TryLoadScene<TEnterData>(SceneType sceneType, TEnterData enterData, CancellationTokenSource cancellationTokenSource) where TEnterData : class, IInitiatorEnterData;
        Awaitable StartScene<TEnterData>(SceneType gamePlayScene, TEnterData enterData, CancellationTokenSource cancellationTokenSource) where TEnterData : class, IInitiatorEnterData;
        Awaitable<bool> TryUnloadScene(SceneType sceneType, CancellationTokenSource cancellationTokenSource);
    }
}
