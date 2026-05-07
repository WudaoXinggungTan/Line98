using System;
using System.Threading;
using UnityEngine;
using Zenject;
using ProjectPlugins.InputSystem;
using CompanyCoreScripts._GameCoreEnterData;
using CompanyCoreScripts.MVC.LogoScreen;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using CompanyCoreScripts.Services.SceneInitiatorsService;
using CompanyCoreScripts.Services.SceneLoaderService;

namespace CompanyCoreScripts._CompanyCoreInitiator
{
    public class CompanyCoreInitiator : MonoBehaviour
    {
        #region Dependencies

        private InputSystem_Actions inputSystemActions;
        private ISceneLoaderService sceneLoaderService;
        private ILogoScreenController logoScreenController;

        #endregion

        #region Constructor

        [Inject]
        private void Constructor(InputSystem_Actions inputSystemActions, ISceneLoaderService sceneLoaderService, ISceneInitiatorsService sceneInitiatorsService,
            ILogoScreenController logoScreenController)
        {
            this.inputSystemActions = inputSystemActions;
            this.sceneLoaderService = sceneLoaderService;
            this.logoScreenController = logoScreenController;
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
                UpdateApplicationSettings();
                InitializeSystems();
                
                _ = LoadGameCoreScene(cancellationTokenSource);
                
                await logoScreenController.Show(cancellationTokenSource);
                await logoScreenController.Hide(cancellationTokenSource);
                await StartGameCoreScene(cancellationTokenSource);
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
        
        private async Awaitable StartGameCoreScene(CancellationTokenSource cancellationTokenSource)
        {
            await sceneLoaderService.StartScene(SceneType.GameCoreScene, new GameCoreInitiatorEnterData(), cancellationTokenSource);
        }

        private void UpdateApplicationSettings()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;
        }

        #endregion
    }
}