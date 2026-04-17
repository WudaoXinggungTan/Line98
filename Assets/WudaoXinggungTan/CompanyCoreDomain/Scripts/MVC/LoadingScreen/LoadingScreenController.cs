using System.Threading;
using UnityEngine;
using Zenject;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.LoggerService;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.MVC.LoadingScreen
{
    public class LoadingScreenController : ILoadingScreenController
    {
        private readonly LoadingScreenView _loadingScreenView;
        
        [Inject]
        public LoadingScreenController(LoadingScreenView loadingScreenView)
        {
            _loadingScreenView = loadingScreenView;
        }

        public void Show()
        {
            MyLogService.LogTopic("Show loading screen", LogTopicType.LoadingScreen );
            _loadingScreenView.ResetSlider();
            _loadingScreenView.Show();
        }

        public void Hide()
        {
            MyLogService.LogTopic("Hide loading screen", LogTopicType.LoadingScreen );
            _loadingScreenView.Hide();
        }
        
        public void ResetSlider()
        {
            _loadingScreenView.ResetSlider();
        }
        
        public async Awaitable SetLoadingSlider(float valueBetween0To1, CancellationTokenSource cancellationTokenSource)
        {
            await _loadingScreenView.SetLoadingSlider(valueBetween0To1, cancellationTokenSource);
        }
    }
}