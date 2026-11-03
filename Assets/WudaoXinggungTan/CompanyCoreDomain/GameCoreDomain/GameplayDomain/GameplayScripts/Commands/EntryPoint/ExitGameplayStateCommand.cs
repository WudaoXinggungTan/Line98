using CompanyCoreScripts.Services.CommandFactoryService.Commands;
using GameplayScripts.MVC.Gameplay.Ball;
using GameplayScripts.MVC.Gameplay.Board;
using GameplayScripts.MVC.UI.HUD;
using GameplayScripts.MVC.UI.Settings;

namespace GameplayScripts.Commands.EntryPoint
{
    public class ExitGameplayStateCommand : BaseCommand, ICommandVoid
    {
        #region Dependencies

        private IHudUICanvasController hudUICanvasController;
        private ISettingsUICanvasController settingsUICanvasController;
        private IBoardController boardController;
        private IBallController ballController;

        #endregion

        #region Public Methods

        public override void ResolveDependencies()
        {
            hudUICanvasController = diContainer.Resolve<IHudUICanvasController>();
            settingsUICanvasController = diContainer.Resolve<ISettingsUICanvasController>();
            boardController = diContainer.Resolve<IBoardController>();
            ballController = diContainer.Resolve<IBallController>();
        }

        public void Execute()
        {
            hudUICanvasController.InitExitPoint();
            settingsUICanvasController.InitExitPoint();
            ballController.InitExitPoint(); // balls sit inside cells, despawn them before the cells are destroyed
            boardController.InitExitPoint();
        }

        #endregion
    }
}
