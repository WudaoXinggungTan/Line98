using System;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using CompanyCoreScripts.Extensions;
using CompanyCoreScripts.Helpers.ObjectPoolsHelpers;

namespace GameplayScripts.MVC.Gameplay.Ball
{
    [RequireComponent(typeof(Image))]
    public class BallView : MonoBehaviour, IPoolable
    {
        #region Constants

        // Pixel-art sprites are drawn on a full cell-sized canvas (padding baked in), so balls and previews both fill
        // their cell and keep the same pixel size as the board. A preview ball is half the diameter of a full one.
        private const float PREVIEW_TO_BALL_SCALE = 0.5f;
        private const float GROW_SECONDS = 0.2f;
        private const float SELECTED_SCALE = 1.15f;
        private const float SELECTED_PULSE_SECONDS = 0.35f;
        private const float MOVE_STEP_SECONDS = 0.05f;
        private const float BLOCKED_SHAKE_SECONDS = 0.25f;
        private const float BLOCKED_SHAKE_DISTANCE = 12f;
        private const int BLOCKED_SHAKE_VIBRATO = 20;

        #endregion

        #region Dependencies

        [SerializeField] private Image image;

        // One sprite per BallColorType, in enum order starting at Red
        [SerializeField] private Sprite[] colorSprites;
        [SerializeField] private Sprite[] previewSprites;

        private BallColorType color;
        private Action despawn;
        private Tween selectedTween;
        private Tween moveTween;
        private Tween blockedTween;
        private Tween growTween;

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
            KillTweens();
        }

        #endregion

        #region Public Methods

        // Balls and previews (next turn's balls) both fill their cell and follow it when the board resizes
        public void Show(BallColorType color, RectTransform cell, bool isPreview = false)
        {
            blockedTween?.Kill();
            growTween?.Kill();
            this.color = color;
            image.sprite = isPreview ? previewSprites[(int)color - 1] : colorSprites[(int)color - 1];

            transform.SetParent(cell, false);
            FillCell();
            transform.localScale = Vector3.one;
        }

        // A preview grows into a full ball where it stands
        public void GrowToFullSize()
        {
            growTween?.Kill();
            image.sprite = colorSprites[(int)color - 1];
            FillCell();
            transform.localScale = Vector3.one * PREVIEW_TO_BALL_SCALE;
            growTween = transform.DOScale(1f, GROW_SECONDS).SetEase(Ease.OutBack);
        }

        // Walks the ball through each cell of the path, one step at a time. Call Show() afterwards to settle it in the last cell.
        public async Awaitable MoveAlongPathAsync(IReadOnlyList<RectTransform> pathCells, CancellationToken cancellationToken)
        {
            // Each ball lives inside its cell and later cells draw on top of earlier ones, so while moving the ball is
            // lifted into the cells' parent (BoardGrid) as the last child, keeping its on-screen size and position
            var ballRect = (RectTransform)transform;
            var worldPosition = ballRect.position;
            var size = ballRect.rect.size;
            ballRect.SetParent(pathCells[0].parent, true);
            ballRect.SetAsLastSibling();
            ballRect.anchorMin = new Vector2(0.5f, 0.5f);
            ballRect.anchorMax = new Vector2(0.5f, 0.5f);
            ballRect.sizeDelta = size;
            ballRect.position = worldPosition;

            var sequence = DOTween.Sequence();
            foreach (var cell in pathCells)
            {
                // Cells pivot on their top-left corner, so aim at the centre of their rect
                var cellCentre = cell.TransformPoint(cell.rect.center);
                sequence.Append(ballRect.DOMove(cellCentre, MOVE_STEP_SECONDS).SetEase(Ease.Linear));
            }

            moveTween = sequence;
            await sequence.WithCancellationSafe(cancellationToken);
        }

        // Short sideways shake when the selected ball can't reach the clicked cell
        public void PlayBlockedFeedback()
        {
            blockedTween?.Complete();
            blockedTween = transform.DOShakePosition(BLOCKED_SHAKE_SECONDS, new Vector3(BLOCKED_SHAKE_DISTANCE, 0f, 0f), BLOCKED_SHAKE_VIBRATO);
        }

        // Selected balls pulse in size until deselected
        public void SetSelected(bool isSelected)
        {
            growTween?.Kill();
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
            KillTweens();
            transform.localScale = Vector3.one;
        }

        #endregion

        #region Private Methods

        private void FillCell()
        {
            var ballRect = (RectTransform)transform;
            ballRect.anchorMin = Vector2.zero;
            ballRect.anchorMax = Vector2.one;
            ballRect.offsetMin = Vector2.zero;
            ballRect.offsetMax = Vector2.zero;
        }

        private void KillTweens()
        {
            selectedTween?.Kill();
            moveTween?.Kill();
            blockedTween?.Kill();
            growTween?.Kill();
            selectedTween = null;
            moveTween = null;
            blockedTween = null;
            growTween = null;
        }

        #endregion
    }
}
