using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Utils;
using CompanyCoreScripts.Services.CommandFactoryService.Commands;
using CompanyCoreScripts.Services.CommandFactoryService.Factory;
using GameCoreScripts._MainMenuEnterData;

namespace MainMenuScripts.Commands.EntryPoint
{
    public class StartMainMenuStateCommand : BaseCommand, ICommandAsync
    {
        private ICommandFactory commandFactory;
        private MainMenuInitiatorEnterData mainMenuInitiatorEnterData;
        
        public StartMainMenuStateCommand SetEnterData(MainMenuInitiatorEnterData mainMenuInitiatorEnterData)
        {
            this.mainMenuInitiatorEnterData = mainMenuInitiatorEnterData;
            return this;
        }
        
        public override void ResolveDependencies()
        {
            commandFactory = diContainer.Resolve<ICommandFactory>();
        }
        
        public Awaitable Execute(CancellationTokenSource cancellationTokenSource)
        {
            return AwaitableUtils.CompletedTask;
        }
    }
}
