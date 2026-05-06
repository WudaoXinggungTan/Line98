using System.Threading;
using CompanyCoreScripts.Extensions;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CompanyCoreScripts.Utils;

namespace CompanyCoreScripts.MVC.LogoScreen
{
    public class LogoScreenView : MonoBehaviour
    {
        #region Dependencies

        [SerializeField] private Canvas logoCanvas;
        [SerializeField] private Image logoBackground;
        [SerializeField] private float logoBackgroundTweenDuration = 0.2f;
        private Tween logoBackgroundTween;

        [SerializeField] private Image[] logoTextImages;
        [SerializeField] private float logoTextTweenDuration = 0.2f;
        [SerializeField] private float logoTextTweenInterval = 0.1f;
        [SerializeField] private Ease logoTextAnimationEase = Ease.OutQuad;

        #endregion

        #region Public Methods

        public async Awaitable Show(CancellationTokenSource cancellationTokenSource)
        {
            logoCanvas.enabled = true;

            await SequenceLogoTextAnimations(cancellationTokenSource);
            logoBackgroundTween = logoBackground.DOFade(1f, logoBackgroundTweenDuration);
            await logoBackgroundTween.WithCancellationSafe(cancellationToken: cancellationTokenSource.Token);
        }
        private async Awaitable SequenceLogoTextAnimations(CancellationTokenSource cancellationTokenSource)
        {
            Sequence logoTextSequence = DOTween.Sequence();
            foreach (Image img in logoTextImages)
            {
                logoTextSequence.Append(img.transform.DOScale(1f, logoTextTweenDuration).From(0f).SetEase(logoTextAnimationEase));
                logoTextSequence.AppendInterval(logoTextTweenInterval);
            }
            await logoTextSequence.WithCancellationSafe(cancellationToken: cancellationTokenSource.Token);
        }
        public void Hide()
        {
            logoCanvas.enabled = false;
        }

        #endregion
    }
}