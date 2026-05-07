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

        [SerializeField] private Canvas loadingCanvas;

        [SerializeField] private AnimatedSlider[] loadingSliderArray;

        [SerializeField] private Image transitionImage;
        [SerializeField] private float transitionMoveDistance = 500f;
        [SerializeField] private float transitionDuration = 1.0f;

        private Tween transitionTween;

        #endregion

        #region Public Methods

        public void InitEntryPoint(Camera uiCamera)
        {
            //loadingSlider.GetSlider().onValueChanged.AddListener(OnSliderValueChanged);
            loadingCanvas.worldCamera = uiCamera;
        }
        
        public void StartEntryPoint()
        {
        }

        public void InitExitPoint()
        {
        }

        public async Awaitable ShowTransition(CancellationTokenSource cancellationTokenSource)
        {
            transitionImage.enabled = true;
            transitionTween?.Kill();
            transitionTween = transitionImage.rectTransform.DOAnchorPosY(transitionMoveDistance, transitionDuration)
                .SetRelative(true)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    transitionImage.DOFade(0, transitionDuration).SetEase(Ease.Linear);
                });

            await transitionTween.WithCancellationSafe(cancellationToken: cancellationTokenSource.Token);
        }
        
        public void Show()
        {
            loadingCanvas.enabled = true;
        }
        public void Hide()
        {
            loadingCanvas.enabled = false;
        }

        public async Awaitable SetLoadingSlider(float valueBetween0To1, CancellationTokenSource cancellationTokenSource)
        {
            Awaitable[] sliderTasks = new Awaitable[loadingSliderArray.Length];
            
            for (int i = 0; i < loadingSliderArray.Length; i++)
            {
                sliderTasks[i] = loadingSliderArray[i].AnimateSliderTo(valueBetween0To1, cancellationTokenSource);
            }
            
            await sliderTasks.WhenAll();
        }

        public void ResetSlider()
        {
            foreach (AnimatedSlider loadingSlider in loadingSliderArray)
            {
                loadingSlider.ResetSlider();
            }
        }

        #endregion
    }
        /*private void OnSliderValueChanged(float value)
        {
            foreach (RectTransform fillArea in fillAreaArray)
            {
                RectTransform rawImage = fillArea.GetComponentInChildren<RectTransform>();

                if (rawImage == null)
                {
                    continue;
                }

                float parentWidth = fillArea.rect.width;
                float parentHeight = fillArea.rect.height;
                float targetHeight = parentHeight * value;

                // SetSizeWithCurrentAnchors is the most reliable way to set dimensions regardless of anchor settings
                rawImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);
            }
        }*/
}