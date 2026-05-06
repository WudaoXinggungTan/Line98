using CompanyCoreScripts.MVC.UISystem;

namespace MainMenuScripts.MVC.UI
{
    public class MainMenuUIGroupController : BaseUIGroupController, IMainMenuUIGroupController
    {
        #region Dependencies

        public MainMenuUIGroupView MainMenuUIGroupView { get; }

        #endregion

        #region Constructor

        public MainMenuUIGroupController(MainMenuUIGroupView mainMenuUIGroupView) : base(mainMenuUIGroupView)
        {
            MainMenuUIGroupView = mainMenuUIGroupView;
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
            MainMenuUIGroupView.SwitchCanvas(MainMenuUIGroupView.MainUICanvasView);
        }

        public void OpenSettingsCanvas()
        {
            MainMenuUIGroupView.SwitchCanvas(MainMenuUIGroupView.SettingsUICanvasView);
        }

        #endregion
    }
}