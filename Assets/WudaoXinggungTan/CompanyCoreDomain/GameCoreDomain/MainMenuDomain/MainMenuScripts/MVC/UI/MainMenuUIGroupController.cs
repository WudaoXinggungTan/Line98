using CompanyCoreScripts.MVC.UISystem;

namespace MainMenuScripts.MVC.UI
{
    public class MainMenuUIGroupController : BaseUIGroupController, IMainMenuUIGroupController
    {
        #region Dependencies

        private readonly MainMenuUIGroupView mainMenuUIGroupView;

        #endregion

        #region Constructor

        public MainMenuUIGroupController(MainMenuUIGroupView mainMenuUIGroupView) : base(mainMenuUIGroupView)
        {
            this.mainMenuUIGroupView = mainMenuUIGroupView;
        }

        #endregion

        #region Public Methods

        public override void InitEntryPoint()
        {
            base.InitEntryPoint();
        }

        public override void StartEntryPoint()
        {
            base.StartEntryPoint();
        }

        public override void InitExitPoint()
        {
            base.InitExitPoint();
        }

        public void OpenMainCanvas()
        {
            mainMenuUIGroupView.SwitchCanvas(mainMenuUIGroupView.MainUICanvasView);
        }

        public void OpenSettingsCanvas()
        {
            mainMenuUIGroupView.SwitchCanvas(mainMenuUIGroupView.SettingsUICanvasView);
        }

        #endregion
    }
}