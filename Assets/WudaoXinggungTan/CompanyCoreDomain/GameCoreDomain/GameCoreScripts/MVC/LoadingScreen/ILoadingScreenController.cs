using System.Threading;
using UnityEngine;

namespace GameCoreScripts.MVC.LoadingScreen
{
    public interface ILoadingScreenController
    {
        public void InitEntryPoint();
        public void StartEntryPoint();
        public void InitExitPoint();
        public Awaitable ShowTransition(CancellationTokenSource cancellationTokenSource);
        public void Show();
        public void Hide();
        public void ResetSlider();
        public Awaitable SetLoadingSlider(float valueBetween0To1, CancellationTokenSource cancellationTokenSource);
    }
}
