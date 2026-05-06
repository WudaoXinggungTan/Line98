using UnityEngine;
using CompanyCoreScripts.MVC.UICamera;

namespace MainMenuScripts.MVC.UI.SettingsCanvas
{
    public class SettingsUICanvasController : ISettingsUICanvasController
    {
        #region Dependencies

        private readonly IUICameraController uiCameraController;
        private readonly IMainMenuUIGroupController mainMenuUIGroupController;
        private readonly SettingsUICanvasView settingsUICanvasView;

        #endregion

        #region Constructor

        public SettingsUICanvasController(IUICameraController uiCameraController, IMainMenuUIGroupController mainMenuUIGroupController, SettingsUICanvasView settingsUICanvasView)
        {
            this.uiCameraController = uiCameraController;
            this.mainMenuUIGroupController = mainMenuUIGroupController;
            this.settingsUICanvasView = settingsUICanvasView;
        }

        #endregion

        #region Public Methods

        public void InitEntryPoint()
        {
            settingsUICanvasView.InitEntryPoint(uiCameraController.UICamera, OnMusicButtonClicked, OnSfxButtonClicked, OnBackButtonClicked);
        }
        
        public void StartEntryPoint()
        {
            settingsUICanvasView.StartEntryPoint();
        }

        public void InitExitPoint()
        {
            settingsUICanvasView.InitExitPoint();
        }

        #endregion

        #region Private Methods

        private void OnMusicButtonClicked()
        {
            Debug.Log("OnMusicButtonClicked");
        }

        private void OnSfxButtonClicked()
        {
            Debug.Log("OnSfxButtonClicked");
        }

        private void OnBackButtonClicked()
        {
            mainMenuUIGroupController.OpenMainCanvas();
        }

        #endregion
    }
}