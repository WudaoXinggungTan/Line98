using CompanyCoreScripts.MVC.Audio;
using CompanyCoreScripts.MVC.UICamera;

namespace GameplayScripts.MVC.UI.Settings
{
    public class SettingsUICanvasController : ISettingsUICanvasController
    {
        #region Dependencies

        private readonly IUICameraController uiCameraController;
        private readonly IGameplayUIGroupController gameplayUIGroupController;
        private readonly SettingsUICanvasView settingsUICanvasView;
        private readonly IAudioController audioController;

        #endregion

        #region Constructor

        public SettingsUICanvasController(IUICameraController uiCameraController, IGameplayUIGroupController gameplayUIGroupController,
            SettingsUICanvasView settingsUICanvasView, IAudioController audioController)
        {
            this.uiCameraController = uiCameraController;
            this.gameplayUIGroupController = gameplayUIGroupController;
            this.settingsUICanvasView = settingsUICanvasView;
            this.audioController = audioController;
        }

        #endregion

        #region Public Methods

        public void InitEntryPoint()
        {
            settingsUICanvasView.InitEntryPoint(uiCameraController.UICamera, OnSoundButtonClicked, OnCloseButtonClicked);
            settingsUICanvasView.SetSoundMuted(audioController.IsMuted);
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

        private void OnSoundButtonClicked()
        {
            audioController.SetMuted(!audioController.IsMuted);
            settingsUICanvasView.SetSoundMuted(audioController.IsMuted);
        }

        private void OnCloseButtonClicked()
        {
            gameplayUIGroupController.CloseSettingsCanvas();
        }

        #endregion
    }
}
