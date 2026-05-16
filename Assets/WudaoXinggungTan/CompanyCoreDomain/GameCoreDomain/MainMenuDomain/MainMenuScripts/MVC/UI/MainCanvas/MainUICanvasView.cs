using System;
using CompanyCoreScripts.MVC.UISystem;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MainMenuScripts.MVC.UI.MainCanvas
{
    public class MainUICanvasView : BaseUICanvasView
    {
        #region Denpendencies

        [SerializeField] private Button startButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private RectTransform buttonsPanel;
        [SerializeField] private RectTransform textPanel;

        private Canvas screenCanvas;

        #endregion

        #region Delegates

        private Action onStartButtonClicked;
        private Action onSettingsButtonClicked;
        private Action onQuitButtonClicked;

        #endregion

        #region Public Methods

        public void InitEntryPoint(Camera uiCamera, Action onStartButtonClicked, Action onSettingsButtonClicked, Action onQuitButtonClicked)
        {
            base.InitEntryPoint();
            screenCanvas = GetComponent<Canvas>();
            screenCanvas.worldCamera = uiCamera;

            startButton.onClick.AddListener(OnStartButtonClicked);
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            exitButton.onClick.AddListener(OnQuitButtonClicked);

            this.onStartButtonClicked = onStartButtonClicked;
            this.onSettingsButtonClicked = onSettingsButtonClicked;
            this.onQuitButtonClicked = onQuitButtonClicked;
        }

        public void StartEntryPoint()
        {
            // Let's do some DOTween animations~
            buttonsPanel.DOAnchorPosX(buttonsPanel.anchoredPosition.x, 1f)
                .From(new Vector2(buttonsPanel.anchoredPosition.x + 540, buttonsPanel.anchoredPosition.y))
                .SetEase(Ease.OutBack);

            textPanel.DOAnchorPosY(textPanel.anchoredPosition.y, 1f)
                .From(new Vector2(textPanel.anchoredPosition.x, textPanel.anchoredPosition.y + 960))
                .SetEase(Ease.OutBack);
        }

        public void InitExitPoint()
        {
            startButton.onClick.RemoveListener(OnStartButtonClicked);
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            exitButton.onClick.RemoveListener(OnQuitButtonClicked);
        }

        #endregion

        #region Private Methods

        private void OnStartButtonClicked()
        {
            onStartButtonClicked?.Invoke();
        }

        private void OnSettingsButtonClicked()
        {
            onSettingsButtonClicked?.Invoke();
        }

        private void OnQuitButtonClicked()
        {
            onQuitButtonClicked?.Invoke();
        }

        #endregion
    }
}