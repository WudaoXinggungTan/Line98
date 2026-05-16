namespace GameplayScripts.MVC.UI
{
    public interface IGameplayUIGroupController
    {
        public void InitEntryPoint();
        public void StartEntryPoint();
        public void InitExitPoint();
        public void OpenSettingsCanvas();
        public void CloseSettingsCanvas();
        public void OpenHowToPlayCanvas();
    }
}