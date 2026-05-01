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
        private IMainMenuUIController mainMenuUIController;
        
        public LoadMainMenuStateCommand SetEnterData(MainMenuInitiatorEnterData mainMenuInitiatorEnterData)
        {
            this.mainMenuInitiatorEnterData = mainMenuInitiatorEnterData;
            return this;
        }
        
        public override void ResolveDependencies()
        {
            mainMenuUIController = diContainer.Resolve<IMainMenuUIController>();
        }

        public Awaitable Execute(CancellationTokenSource cancellationTokenSource)
        {
            return AwaitableUtils.CompletedTask;
        }
    }
}
