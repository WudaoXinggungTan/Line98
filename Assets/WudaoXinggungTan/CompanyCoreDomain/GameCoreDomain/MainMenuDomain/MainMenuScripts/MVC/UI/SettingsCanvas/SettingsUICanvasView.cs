using System;
using UnityEngine;
using UnityEngine.UI;
using CompanyCoreScripts.MVC.UISystem;

namespace MainMenuScripts.MVC.UI.SettingsCanvas
{
    public class SettingsUICanvasView : BaseUICanvasView
    {
        #region Denpendencies

        [SerializeField] private Button musicButton;
        [SerializeField] private Button sfxButton;
        [SerializeField] private Button backButton;
        private Canvas screenCanvas;

        #endregion

        #region Delegates

        private Action onMusicButtonClicked;
        private Action onSfxButtonClicked;
        private Action onBackButtonClicked;

        #endregion

        #region Public Methods

        public void InitEntryPoint(Camera uiCamera, Action onMusicButtonClicked, Action onSfxButtonClicked, Action onBackButtonClicked)
        {
            base.InitEntryPoint();
            screenCanvas = GetComponent<Canvas>();
            screenCanvas.worldCamera = uiCamera;
            
            musicButton.onClick.AddListener(OnMusicButtonClicked);
            sfxButton.onClick.AddListener(OnSfxButtonClicked);
            backButton.onClick.AddListener(OnBackButtonClicked);

            this.onMusicButtonClicked = onMusicButtonClicked;
            this.onSfxButtonClicked = onSfxButtonClicked;
            this.onBackButtonClicked = onBackButtonClicked;
        }
        
        public new void StartEntryPoint()
        {
            base.StartEntryPoint();
        }

        public new void InitExitPoint()
        {
            base.InitExitPoint();
            musicButton.onClick.RemoveListener(OnMusicButtonClicked);
            sfxButton.onClick.RemoveListener(OnSfxButtonClicked);
            backButton.onClick.RemoveListener(OnBackButtonClicked);
        }

        #endregion

        #region Private Methods
        
        private void OnMusicButtonClicked()
        {
            onMusicButtonClicked?.Invoke();
        }

        private void OnSfxButtonClicked()
        {
            onSfxButtonClicked?.Invoke();
        }

        private void OnBackButtonClicked()
        {
            onBackButtonClicked?.Invoke();
        }

        #endregion
    }
}