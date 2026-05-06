using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CompanyCoreScripts.Extensions;
using CompanyCoreScripts.Helpers.UIHelpers;
using CompanyCoreScripts.Utils;

namespace GameCoreScripts.MVC.LoadingScreen
{
    public class LoadingScreenView : MonoBehaviour
    {
        #region Dependencies

        [SerializeField] private AnimatedSlider loadingSlider;
        [SerializeField] private Canvas loadingCanvas;
        [SerializeField] private Image transitionImage;
        [SerializeField] private float transitionMoveDistance = 500f;
        [SerializeField] private float transitionDuration = 1.0f;

        private Tween transitionTween;
        #endregion

        #region Public Methods

        public async Awaitable SetLoadingSlider(float valueBetween0To1, CancellationTokenSource cancellationTokenSource)
        {
            await loadingSlider.AnimateSliderTo(valueBetween0To1, cancellationTokenSource);
        }

        public async Awaitable Show(CancellationTokenSource cancellationTokenSource)
        {
            loadingCanvas.enabled = true;
            transitionImage.enabled = true;
            transitionTween = transitionImage.rectTransform.DOAnchorPosY(transitionMoveDistance, transitionDuration)
                .SetRelative(true)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    transitionImage.DOFade(0, transitionDuration)
                        .SetEase(Ease.Linear);
                });

            await transitionTween.WithCancellationSafe(cancellationToken: cancellationTokenSource.Token);
        }

        public void Hide()
        {
            loadingCanvas.enabled = false;
        }

        public void ResetSlider()
        {
            loadingSlider.ResetSlider();
        }

        #endregion
    }
}