using CompanyCoreScripts.Services.CommandFactoryService.Commands;
using GameplayScripts.MVC.UI.HUD;

namespace GameplayScripts.Commands.EntryPoint
{
    public class ExitGameplayStateCommand : BaseCommand, ICommandVoid
    {
        #region Dependencies

        private IHudUICanvasController hudUICanvasController;

        #endregion
        
        #region Public Methods

        public override void ResolveDependencies()
        {
            hudUICanvasController = diContainer.Resolve<IHudUICanvasController>();
        }

        public void Execute()
        {
            hudUICanvasController.InitExitPoint();
        }

        #endregion
    }
}