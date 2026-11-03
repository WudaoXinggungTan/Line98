using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CompanyCoreScripts.MVC.UISystem;

namespace GameplayScripts.MVC.UI.Settings
{
    // Settings panel shown over the HUD during gameplay (opened by the HUD's Settings button)
    public class SettingsUICanvasView : BaseUICanvasView
    {
        #region Constants

        private const string SOUND_ON_TEXT = "Sound: On";
        private const string SOUND_MUTED_TEXT = "Sound: Off";

        #endregion

        #region Dependencies

        [SerializeField] private Button soundButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private TMP_Text soundLabelText;

        private Canvas screenCanvas;

        #endregion

        #region Actions

        private Action onSoundButtonClicked;
        private Action onCloseButtonClicked;

        #endregion

        #region Public Methods

        public void InitEntryPoint(Camera uiCamera, Action onSoundButtonClicked, Action onCloseButtonClicked)
        {
            base.InitEntryPoint();
            screenCanvas = GetComponent<Canvas>();
            screenCanvas.worldCamera = uiCamera;

            soundButton.onClick.AddListener(OnSoundButtonClicked);
            closeButton.onClick.AddListener(OnCloseButtonClicked);

            this.onSoundButtonClicked = onSoundButtonClicked;
            this.onCloseButtonClicked = onCloseButtonClicked;
        }

        public override void StartEntryPoint()
        {
            base.StartEntryPoint();
        }

        public override void InitExitPoint()
        {
            base.InitExitPoint();
            soundButton.onClick.RemoveListener(OnSoundButtonClicked);
            closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        public override void StartCanvas()
        {
            base.StartCanvas();
        }

        public override void CloseCanvas()
        {
            base.CloseCanvas();
        }

        public void SetSoundMuted(bool isMuted)
        {
            soundLabelText.text = isMuted ? SOUND_MUTED_TEXT : SOUND_ON_TEXT;
        }

        #endregion

        #region Private Methods

        private void OnSoundButtonClicked()
        {
            onSoundButtonClicked?.Invoke();
        }

        private void OnCloseButtonClicked()
        {
            onCloseButtonClicked?.Invoke();
        }

        #endregion
    }
}
