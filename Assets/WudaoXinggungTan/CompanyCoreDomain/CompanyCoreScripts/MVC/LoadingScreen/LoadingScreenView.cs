using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Helpers.UIHelpers;

namespace CompanyCoreScripts.MVC.LoadingScreen
{
    public class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private AnimatedSlider loadingSlider;
        [SerializeField] private Canvas loadingScreenCanvas;
        
        public async Awaitable SetLoadingSlider(float valueBetween0To1, CancellationTokenSource cancellationTokenSource)
        {
            await loadingSlider.AnimateSliderTo(valueBetween0To1, cancellationTokenSource);
        }

        public void Show()
        {
            loadingScreenCanvas.enabled = true;
        }

        public void Hide()
        {
            loadingScreenCanvas.enabled = false;
        }

        public void ResetSlider()
        {
            loadingSlider.ResetSlider();
        }
    }
}
