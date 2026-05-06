using System;
using CompanyCoreScripts.MVC.UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuScripts.MVC.UI.MainCanvas
{
    public class MainUICanvasView : BaseUICanvasView
    {
        #region Denpendencies

        [SerializeField] private Button startButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;
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