namespace CompanyCoreScripts.MVC.UISystem
{
    public interface IBaseUIGroupController
    {
        public BaseUIGroupView BaseUIGroupView { get; }
        public void InitEntryPoint();
        public void InitExitPoint();
    }
}