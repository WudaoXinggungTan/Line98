using System.Threading;
using UnityEngine;

namespace GameCoreScripts.MVC.LoadingScreen
{
    public interface ILoadingScreenController
    {
        public Awaitable Show(CancellationTokenSource cancellationTokenSource);
        public void Hide();
        public void ResetSlider();
        public Awaitable SetLoadingSlider(float valueBetween0To1, CancellationTokenSource cancellationTokenSource);
    }
}
