using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using CompanyCoreScripts.Services.SceneInitiatorsService;

namespace CompanyCoreScripts.Services.SceneLoaderService
{
    public class SceneLoaderService : ISceneLoaderService
    {
        #region Dependencies

        private readonly ISceneInitiatorsService sceneInitiatorsService;
        private readonly HashSet<string> loadedScenes = new();
        private readonly HashSet<string> loadingScenes = new();

        #endregion

        #region Constructor

        public SceneLoaderService(ISceneInitiatorsService sceneInitiatorsService)
        {
            this.sceneInitiatorsService = sceneInitiatorsService;
            AddOpenedScenesToLoadedHashset();
        }

        #endregion

        #region Private Methods

        private void AddOpenedScenesToLoadedHashset()
        {
            var countLoaded = SceneManager.sceneCount;

            for (var i = 0; i < countLoaded; i++)
            {
                var sceneName = SceneManager.GetSceneAt(i).name;
                loadedScenes.Add(sceneName);
            }
        }

        private async Awaitable LoadScene(string sceneName, CancellationTokenSource cancellationTokenSource)
        {
            loadingScenes.Add(sceneName);
            cancellationTokenSource.Token.ThrowIfCancellationRequested();

            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            cancellationTokenSource.Token.ThrowIfCancellationRequested();
            loadingScenes.Remove(sceneName);
            loadedScenes.Add(sceneName);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }

        private async Awaitable UnloadScene(SceneType sceneType, CancellationTokenSource cancellationTokenSource)
        {
            await sceneInitiatorsService.InvokeInitiatorExitPoint(sceneType, cancellationTokenSource);
            var sceneName = sceneType.ToString();
            await SceneManager.UnloadSceneAsync(sceneName);
            loadedScenes.Remove(sceneName);
        }

        #endregion

        #region Public Methods

        public async Awaitable<bool> TryLoadScene(string sceneName, CancellationTokenSource cancellationTokenSource)
        {
            var isSceneAlreadyLoaded = loadedScenes.Contains(sceneName);

            if (isSceneAlreadyLoaded)
            {
                MyLoggerService.LogError($"scene:{sceneName} is already Loaded");
                return false;
            }

            var isSceneAlreadyLoading = loadingScenes.Contains(sceneName);

            if (isSceneAlreadyLoading)
            {
                MyLoggerService.LogError($"scene:{sceneName} is already Loading");
                return false;
            }

            await LoadScene(sceneName, cancellationTokenSource);
            return true;
        }

        public async Awaitable<bool> TryLoadScene<TEnterData>(SceneType sceneType, TEnterData enterData, CancellationTokenSource cancellationTokenSource) where TEnterData : class, IInitiatorEnterData
        {
            if (!await TryLoadScene(sceneType.ToString(), cancellationTokenSource))
            {
                return false;
            }

            await sceneInitiatorsService.InvokeInitiatorLoadEntryPoint(sceneType, enterData, cancellationTokenSource);
            return true;
        }

        public async Awaitable StartScene<TEnterData>(SceneType sceneType, TEnterData enterData, CancellationTokenSource cancellationTokenSource) where TEnterData : class, IInitiatorEnterData
        {
            await sceneInitiatorsService.InvokeInitiatorStartEntryPoint(sceneType, enterData, cancellationTokenSource);
        }

        public async Awaitable<bool> TryUnloadScene(SceneType sceneType, CancellationTokenSource cancellationTokenSource)
        {
            var sceneName = sceneType.ToString();
            var isSceneAlreadyLoaded = loadedScenes.Contains(sceneName);

            if (!isSceneAlreadyLoaded)
            {
                MyLoggerService.LogError($"scene:{sceneName} cant be unloaded as it is not Loaded");
                return false;
            }

            var isSceneAlreadyLoading = loadingScenes.Contains(sceneName);

            if (isSceneAlreadyLoading)
            {
                MyLoggerService.LogError($"scene:{sceneName} cant be unloaded as it during Loading");
                return false;
            }

            await UnloadScene(sceneType, cancellationTokenSource);
            return true;
        }

        #endregion
    }
}