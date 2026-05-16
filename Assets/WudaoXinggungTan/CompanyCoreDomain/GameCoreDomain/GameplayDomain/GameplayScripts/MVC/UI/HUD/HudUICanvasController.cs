using CompanyCoreScripts.MVC.UICamera;

namespace GameplayScripts.MVC.UI.HUD
{
    public class HudUICanvasController : IHudUICanvasController
    {
        #region Dependencies

        private readonly IUICameraController uiCameraController;
        private readonly IGameplayUIGroupController gameplayUIGroupController;
        private readonly HudUICanvasView hudUICanvasView;

        #endregion

        #region Constructor

        public HudUICanvasController(IUICameraController uiCameraController, IGameplayUIGroupController gameplayUIGroupController, HudUICanvasView hudUICanvasView)
        {
            this.uiCameraController = uiCameraController;
            this.gameplayUIGroupController = gameplayUIGroupController;
            this.hudUICanvasView = hudUICanvasView;
        }

        #endregion

        #region Public Methods

        public void InitEntryPoint()
        {
            hudUICanvasView.InitEntryPoint(uiCameraController.UICamera);
        }

        public void StartEntryPoint()
        {
            hudUICanvasView.StartEntryPoint();
        }

        public void InitExitPoint()
        {
            hudUICanvasView.InitExitPoint();
        }

        #endregion
    }
}