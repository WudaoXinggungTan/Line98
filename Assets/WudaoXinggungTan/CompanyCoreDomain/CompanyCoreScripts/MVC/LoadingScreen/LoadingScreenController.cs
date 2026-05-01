using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.LoggerService;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using Zenject;

namespace CompanyCoreScripts.MVC.LoadingScreen
{
    public class LoadingScreenController : ILoadingScreenController
    {
        #region Dependency (View)

        private readonly LoadingScreenView loadingScreenView;

        #endregion

        #region Constructor

        public LoadingScreenController(LoadingScreenView loadingScreenView)
        {
            this.loadingScreenView = loadingScreenView;
        }

        #endregion

        #region Public Methods

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
