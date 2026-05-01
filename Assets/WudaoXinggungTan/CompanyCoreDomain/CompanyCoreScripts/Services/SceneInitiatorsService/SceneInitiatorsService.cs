using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.SceneLoaderService;

namespace CompanyCoreScripts.Services.SceneInitiatorsService
{
    public class SceneInitiatorsService : ISceneInitiatorsService
    {
        #region Variables

        private readonly Dictionary<SceneType, ISceneInitiator> sceneInitiatorsDictionary = new Dictionary<SceneType, ISceneInitiator>();

        #endregion

        #region Public Methods

        public void RegisterInitiator(ISceneInitiator sceneInitiator)
        {
            sceneInitiatorsDictionary.Add(sceneInitiator.SceneType, sceneInitiator);
        }

        public void UnregisterInitiator(ISceneInitiator sceneInitiator)
        {
            sceneInitiatorsDictionary.Remove(sceneInitiator.SceneType);
        }

        public async Awaitable InvokeInitiatorLoadEntryPoint(SceneType sceneType, IInitiatorEnterData enterData, CancellationTokenSource cancellationTokenSource)
        {
            await sceneInitiatorsDictionary[sceneType].LoadEntryPoint(enterData, cancellationTokenSource);
        }

        public async Awaitable InvokeInitiatorStartEntryPoint(SceneType sceneType, IInitiatorEnterData enterData, CancellationTokenSource cancellationTokenSource)
        {
            await sceneInitiatorsDictionary[sceneType].StartEntryPoint(enterData, cancellationTokenSource);
        }

        public async Awaitable InvokeInitiatorExitPoint(SceneType sceneType, CancellationTokenSource cancellationTokenSource)
        {
            await sceneInitiatorsDictionary[sceneType].InitExitPoint(cancellationTokenSource);
        }

        #endregion
    }
}
