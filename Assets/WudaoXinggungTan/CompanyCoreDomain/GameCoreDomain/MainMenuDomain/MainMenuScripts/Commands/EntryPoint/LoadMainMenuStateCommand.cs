using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.CommandFactoryService.Commands;
using CompanyCoreScripts.Utils;
using GameCoreScripts._MainMenuEnterData;
using MainMenuScripts.MVC.UI.MainMenu;

namespace MainMenuScripts.Commands.EntryPoint
{
    public class LoadMainMenuStateCommand : BaseCommand, ICommandAsync
    {
        private MainMenuInitiatorEnterData mainMenuInitiatorEnterData;
        private IMainMenuUIGroupController mainMenuUIGroupController;
        private IMainMenuUICanvasController mainMenuUICanvasController;
        
        public LoadMainMenuStateCommand SetEnterData(MainMenuInitiatorEnterData mainMenuInitiatorEnterData)
        {
            this.mainMenuInitiatorEnterData = mainMenuInitiatorEnterData;
            return this;
        }
        
        public override void ResolveDependencies()
        {
            mainMenuUIGroupController = diContainer.Resolve<IMainMenuUIGroupController>();
            mainMenuUICanvasController = diContainer.Resolve<IMainMenuUICanvasController>();
        }

        public Awaitable Execute(CancellationTokenSource cancellationTokenSource)
        {
            mainMenuUIGroupController.InitEntryPoint();
            mainMenuUICanvasController.InitEntryPoint();
            return AwaitableUtils.CompletedTask;
        }
    }
}
