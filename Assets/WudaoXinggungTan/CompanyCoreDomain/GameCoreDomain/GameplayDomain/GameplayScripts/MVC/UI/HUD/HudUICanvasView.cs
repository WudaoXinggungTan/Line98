using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using CompanyCoreScripts.MVC.UISystem;
using GameplayScripts.MVC.Gameplay.Ball;

namespace GameplayScripts.MVC.UI.HUD
{
    public class HudUICanvasView : BaseUICanvasView
    {
        #region Dependencies

        [SerializeField] private Button restartButton;
        [SerializeField, FormerlySerializedAs("soundButton")] private Button settingsButton;
        [SerializeField] private Button undoButton;
        [SerializeField] private Button newGameButton;

        [SerializeField] private TMP_Text scoreValueText;
        [SerializeField] private TMP_Text bestValueText;

        [SerializeField] private Image[] nextBallSlots;

        // One sprite per BallColorType, in enum order starting at Red (the same sprites as the Ball prefab)
        [SerializeField] private Sprite[] ballSprites;

        private Canvas screenCanvas;
        private Sprite emptySlotSprite;

        #endregion

        #region Actions

        private Action onRestartButtonClicked;
        private Action onSettingsButtonClicked;
        private Action onUndoButtonClicked;
        private Action onNewGameButtonClicked;

        #endregion

        #region Public Methods

        public void InitEntryPoint(Camera uiCamera, Action onRestartButtonClicked, Action onSettingsButtonClicked, Action onUndoButtonClicked, Action onNewGameButtonClicked)
        {
            base.InitEntryPoint();
            screenCanvas = GetComponent<Canvas>();
            screenCanvas.worldCamera = uiCamera;

            // The empty-circle look of a slot, shown again when there is no next ball for it
            emptySlotSprite = nextBallSlots.Length > 0 ? nextBallSlots[0].sprite : null;

            restartButton.onClick.AddListener(OnRestartButtonClicked);
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            undoButton.onClick.AddListener(OnUndoButtonClicked);
            newGameButton.onClick.AddListener(OnNewGameButtonClicked);

            this.onRestartButtonClicked = onRestartButtonClicked;
            this.onSettingsButtonClicked = onSettingsButtonClicked;
            this.onUndoButtonClicked = onUndoButtonClicked;
            this.onNewGameButtonClicked = onNewGameButtonClicked;
        }

        public override void StartEntryPoint()
        {
            base.StartEntryPoint();
        }

        public override void InitExitPoint()
        {
            base.InitExitPoint();
            restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            undoButton.onClick.RemoveListener(OnUndoButtonClicked);
            newGameButton.onClick.RemoveListener(OnNewGameButtonClicked);
        }

        public override void StartCanvas()
        {
            base.StartCanvas();
        }

        public override void CloseCanvas()
        {
            base.CloseCanvas();
        }

        public void SetScore(int score)
        {
            scoreValueText.text = score.ToString();
        }

        public void SetBestScore(int bestScore)
        {
            bestValueText.text = bestScore.ToString();
        }

        public void SetUndoInteractable(bool isInteractable)
        {
            undoButton.interactable = isInteractable;
        }

        public void ShowNextBalls(IReadOnlyList<BallColorType> colors)
        {
            for (var i = 0; i < nextBallSlots.Length; i++)
            {
                nextBallSlots[i].sprite = i < colors.Count ? ballSprites[(int)colors[i] - 1] : emptySlotSprite;
            }
        }

        #endregion

        #region Private Methods

        private void OnRestartButtonClicked()
        {
            onRestartButtonClicked?.Invoke();
        }

        private void OnSettingsButtonClicked()
        {
            onSettingsButtonClicked?.Invoke();
        }

        private void OnUndoButtonClicked()
        {
            onUndoButtonClicked?.Invoke();
        }

        private void OnNewGameButtonClicked()
        {
            onNewGameButtonClicked?.Invoke();
        }

        #endregion
    }
}
