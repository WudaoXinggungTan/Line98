using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CompanyCoreScripts.Extensions;

namespace CompanyCoreScripts.Helpers.UIHelpers
{
    public class AnimatedSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private Ease animationEase = Ease.OutQuad;

        private Tween currentAnimationTween;

        public async Awaitable AnimateSliderTo(float targetValueBetween0To1, CancellationTokenSource cancellationTokenSource)
        {
            currentAnimationTween?.Kill();
            currentAnimationTween = slider.DOValue(targetValueBetween0To1, animationDuration).SetEase(animationEase);
            await currentAnimationTween.WithCancellationSafe(cancellationToken: cancellationTokenSource.Token);
        }

        public void ResetSlider()
        {
            currentAnimationTween?.Kill();
            slider.value = 0;
        }
    }
}
