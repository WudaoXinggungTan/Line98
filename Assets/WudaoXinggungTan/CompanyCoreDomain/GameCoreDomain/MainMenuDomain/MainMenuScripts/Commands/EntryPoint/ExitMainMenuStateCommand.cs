using CompanyCoreScripts.Services.CommandFactoryService.Commands;
using MainMenuScripts.MVC.UI.MainCanvas;

namespace MainMenuScripts.Commands.EntryPoint
{
    public class ExitMainMenuStateCommand : BaseCommand, ICommandVoid
    {
        private IMainUICanvasController mainUICanvasController;
        
        public override void ResolveDependencies()
        {
            mainUICanvasController = diContainer.Resolve<IMainUICanvasController>();
        }

        public void Execute()
        {
            // Dispose classes that need dispose
            // For example: RemoveAudioClip from SO
            // Dispose level from memory
            // Disable gameplay input (still keep ui input)
            // call init exit point for lower controllers
            
            //For this main menu, prob tell canvas controllers to tell buttons controllers to -= events and delete all template children (if like there is a level grid that clones level buttons)
            mainUICanvasController.InitExitPoint();
        }
    }
}
