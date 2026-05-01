using CompanyCoreScripts.MVC.UICamera;

namespace MainMenuScripts.MVC.UI.MainMenu
{
    public class MainMenuUIController : IMainMenuUIController
    {
        private readonly IUICameraController uiCameraController;
        private readonly MainMenuUIGroupView mainMenuUIGroupView;

        public MainMenuUIController(IUICameraController uiCameraController, MainMenuUIGroupView mainMenuUIGroupView)
        {
            this.uiCameraController = uiCameraController;
            this.mainMenuUIGroupView = mainMenuUIGroupView;
        }

        public void InitEntryPoint()
        {
            mainMenuUIGroupView.InitEntryPoint(uiCameraController.UICamera);
        }
        
        
    }
}