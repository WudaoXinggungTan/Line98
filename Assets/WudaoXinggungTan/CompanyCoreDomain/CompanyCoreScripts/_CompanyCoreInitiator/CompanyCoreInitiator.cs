using System;
using System.Threading;
using UnityEngine;
using Zenject;
using ProjectPlugins.InputSystem;
using CompanyCoreScripts._GameCoreEnterData;
using CompanyCoreScripts.MVC.LoadingScreen;
using CompanyCoreScripts.Services.LoggerService.Interface;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using CompanyCoreScripts.Services.SceneInitiatorsService;
using CompanyCoreScripts.Services.SceneLoaderService;

namespace CompanyCoreScripts._CompanyCoreInitiator
{
    public class CompanyCoreInitiator : MonoBehaviour
    {
        #region Dependencies

        private InputSystem_Actions inputSystemActions;
        private ILoadingScreenController loadingScreenController;
        private ISceneLoaderService sceneLoaderService;

        #endregion

        #region Constructor

        [Inject]
        private void Constructor(InputSystem_Actions inputSystemActions, ILoadingScreenController loadingScreenController, 
            ISceneLoaderService sceneLoaderService, ISceneInitiatorsService sceneInitiatorsService)
        {
            this.inputSystemActions = inputSystemActions;
            this.loadingScreenController = loadingScreenController;
            this.sceneLoaderService = sceneLoaderService;
        }

        #endregion

        #region Private Methods

        private void Start()
        {
            _ = InitEntryPoint(CancellationTokenSource.CreateLinkedTokenSource(Application.exitCancellationToken));
        }

        private async Awaitable InitEntryPoint(CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                loadingScreenController.ResetSlider();
                loadingScreenController.Show();

                UpdateApplicationSettings();
                InitializeSystems();
                await LoadGameCoreScene(cancellationTokenSource);

                await loadingScreenController.SetLoadingSlider(1f, cancellationTokenSource);
                loadingScreenController.Hide();
            }
            catch (OperationCanceledException)
            {
                MyLoggerService.Log("Operation init core was cancelled");
            }
            catch (Exception exception)
            {
                MyLoggerService.LogException(exception);
                throw;
            }
        }

        private void InitializeSystems()
        {
            inputSystemActions.Enable();
        }

        private async Awaitable LoadGameCoreScene(CancellationTokenSource cancellationTokenSource)
        {
            await sceneLoaderService.TryLoadScene(SceneType.GameCoreScene, new GameCoreInitiatorEnterData(), cancellationTokenSource);
        }

        private void UpdateApplicationSettings()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;
        }

        #endregion
    }
}