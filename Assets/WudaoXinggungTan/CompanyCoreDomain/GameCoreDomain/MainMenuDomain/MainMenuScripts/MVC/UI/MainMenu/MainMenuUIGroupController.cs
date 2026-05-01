
namespace MainMenuScripts.MVC.UI.MainMenu
{
    public class MainMenuUIGroupController : IMainMenuUIGroupController
    {
        private readonly MainMenuUIGroupView mainMenuUIGroupView;

        public MainMenuUIGroupController(MainMenuUIGroupView mainMenuUIGroupView)
        {
            this.mainMenuUIGroupView = mainMenuUIGroupView;
        }

        public void InitEntryPoint()
        {
            mainMenuUIGroupView.InitEntryPoint();
        }

        public void ExitEntryPoint()
        {
            mainMenuUIGroupView.ExitEntryPoint();
        }
    }
}