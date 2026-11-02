using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using CompanyCoreScripts.Helpers.ObjectPoolsHelpers;

namespace GameplayScripts.MVC.Gameplay.Ball
{
    [RequireComponent(typeof(Image))]
    public class BallView : MonoBehaviour, IPoolable
    {
        #region Constants

        private const float SELECTED_SCALE = 1.15f;
        private const float SELECTED_PULSE_SECONDS = 0.35f;

        #endregion

        #region Dependencies

        [SerializeField] private Image image;

        // One sprite per BallColorType, in enum order starting at Red
        [SerializeField] private Sprite[] colorSprites;

        private Action despawn;
        private Tween selectedTween;

        #endregion

        #region Properties

        public Action Despawn
        {
            set => despawn = value;
        }

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            selectedTween?.Kill();
        }

        #endregion

        #region Public Methods

        public void Show(BallColorType color, RectTransform cell)
        {
            image.sprite = colorSprites[(int)color - 1];

            // Ball fills 80% of the cell, centred, and follows the cell when the board resizes
            var ballRect = (RectTransform)transform;
            ballRect.SetParent(cell, false);
            ballRect.anchorMin = new Vector2(0.1f, 0.1f);
            ballRect.anchorMax = new Vector2(0.9f, 0.9f);
            ballRect.offsetMin = Vector2.zero;
            ballRect.offsetMax = Vector2.zero;
            ballRect.localScale = Vector3.one;
        }

        // Selected balls pulse in size until deselected
        public void SetSelected(bool isSelected)
        {
            selectedTween?.Kill();
            selectedTween = null;
            transform.localScale = Vector3.one;

            if (isSelected)
            {
                selectedTween = transform.DOScale(SELECTED_SCALE, SELECTED_PULSE_SECONDS)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo);
            }
        }

        public void Release()
        {
            despawn?.Invoke();
        }

        public void OnCreated()
        {
        }

        public void OnSpawned()
        {
            gameObject.SetActive(true);
        }

        public void OnDespawned()
        {
            SetSelected(false);
        }

        #endregion
    }
}
