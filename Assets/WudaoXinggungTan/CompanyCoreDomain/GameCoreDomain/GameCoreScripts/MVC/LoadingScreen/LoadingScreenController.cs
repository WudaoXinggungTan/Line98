using System.Threading;
using UnityEngine;
using CompanyCoreScripts.MVC.UICamera;
using CompanyCoreScripts.Services.LoggerService;
using CompanyCoreScripts.Services.LoggerService.StaticClass;

namespace GameCoreScripts.MVC.LoadingScreen
{
    public class LoadingScreenController : ILoadingScreenController
    {
        #region Dependency

        private readonly LoadingScreenView loadingScreenView;
        private readonly IUICameraController uiCameraController;


        #endregion

        #region Constructor

        public LoadingScreenController(IUICameraController uiCameraController, LoadingScreenView loadingScreenView)
        {
            this.uiCameraController = uiCameraController;
            this.loadingScreenView = loadingScreenView;
        }

        #endregion

        #region Public Methods

        public void InitEntryPoint()
        {
            loadingScreenView.InitEntryPoint(uiCameraController.UICamera);
        }
        
        public void StartEntryPoint()
        {
            loadingScreenView.StartEntryPoint();
        }

        public void InitExitPoint()
        {
            loadingScreenView.InitExitPoint();
        }

        public async Awaitable ShowTransition(CancellationTokenSource cancellationTokenSource)
        {
            await loadingScreenView.ShowTransition(cancellationTokenSource);
        }

        public void Show()
        {
            MyLoggerService.LogTopic("Show loading screen", LogTopicType.LoadingScreen);
            loadingScreenView.ResetSlider();
            loadingScreenView.Show();
        }

        public void Hide()
        {
            MyLoggerService.LogTopic("Hide loading screen", LogTopicType.LoadingScreen);
            loadingScreenView.Hide();
        }

        public void ResetSlider()
        {
            loadingScreenView.ResetSlider();
        }

        public async Awaitable SetLoadingSlider(float valueBetween0To1, CancellationTokenSource cancellationTokenSource)
        {
            await loadingScreenView.SetLoadingSlider(valueBetween0To1, cancellationTokenSource);
        }

        #endregion
    }
}