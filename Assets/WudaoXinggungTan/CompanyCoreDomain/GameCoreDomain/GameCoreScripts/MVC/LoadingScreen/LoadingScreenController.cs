using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.LoggerService;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using CompanyCoreScripts.Utils;

namespace GameCoreScripts.MVC.LoadingScreen
{
    public class LoadingScreenController : ILoadingScreenController
    {
        #region Dependency

        private readonly LoadingScreenView loadingScreenView;

        #endregion

        #region Constructor

        public LoadingScreenController(LoadingScreenView loadingScreenView)
        {
            this.loadingScreenView = loadingScreenView;
        }

        #endregion

        #region Public Methods

        public async Awaitable Show(CancellationTokenSource cancellationTokenSource)
        {
            MyLoggerService.LogTopic("Show loading screen", LogTopicType.LoadingScreen);
            loadingScreenView.ResetSlider();
            await loadingScreenView.Show(cancellationTokenSource);
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
