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
        private CanvasGroup screenCanvasGroup;

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
            screenCanvasGroup = GetComponent<CanvasGroup>();
            screenCanvas.worldCamera = uiCamera;

            startButton.onClick.AddListener(OnStartButtonClicked);
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            exitButton.onClick.AddListener(OnQuitButtonClicked);

            this.onStartButtonClicked = onStartButtonClicked;
            this.onSettingsButtonClicked = onSettingsButtonClicked;
            this.onQuitButtonClicked = onQuitButtonClicked;
        }

        public override void StartEntryPoint()
        {
            base.StartEntryPoint();
            // Let's do some DOTween animations~
            screenCanvasGroup.DOFade(1f, 1f).From(0f);
            
            buttonsPanel.DOAnchorPosX(buttonsPanel.anchoredPosition.x, 1f)
                .From(new Vector2(buttonsPanel.anchoredPosition.x + 540, buttonsPanel.anchoredPosition.y))
                .SetEase(Ease.OutBack);

            textPanel.DOAnchorPosY(textPanel.anchoredPosition.y, 1f)
                .From(new Vector2(textPanel.anchoredPosition.x, textPanel.anchoredPosition.y + 960))
                .SetEase(Ease.OutBack);
        }

        public override void InitExitPoint()
        {
            base.InitExitPoint();
            startButton.onClick.RemoveListener(OnStartButtonClicked);
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            exitButton.onClick.RemoveListener(OnQuitButtonClicked);
        }

        public override void StartCanvas()
        {
            base.StartCanvas();
        }
        
        public override void CloseCanvas()
        {
            base.CloseCanvas();
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