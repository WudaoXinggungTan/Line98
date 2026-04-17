using System.Threading;
using UnityEngine;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.InitiatorInvokerService;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.SceneService
{
    public interface ISceneLoaderService
    {
        void InitEntryPoint();
        Awaitable<bool> TryLoadScene<TEnterData>(SceneType sceneType, TEnterData enterData, CancellationTokenSource cancellationTokenSource) where TEnterData : class, IInitiatorEnterData;
        Awaitable StartScene<TEnterData>(SceneType gamePlayScene, TEnterData enterData, CancellationTokenSource cancellationTokenSource) where TEnterData : class, IInitiatorEnterData;
        Awaitable<bool> TryUnloadScene(SceneType sceneType, CancellationTokenSource cancellationTokenSource);
    }
}