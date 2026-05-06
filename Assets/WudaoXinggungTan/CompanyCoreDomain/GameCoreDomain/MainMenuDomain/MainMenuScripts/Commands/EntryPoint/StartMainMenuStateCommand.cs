using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Utils;
using CompanyCoreScripts.Services.CommandFactoryService.Commands;
using GameCoreScripts._MainMenuEnterData;
using MainMenuScripts.MVC.UI;
using MainMenuScripts.MVC.UI.MainCanvas;
using MainMenuScripts.MVC.UI.SettingsCanvas;

namespace MainMenuScripts.Commands.EntryPoint
{
    public class StartMainMenuStateCommand : BaseCommand, ICommandAsync
    {
        private MainMenuInitiatorEnterData mainMenuInitiatorEnterData;        
        private IMainMenuUIGroupController mainMenuUIGroupController;
        private IMainUICanvasController mainUICanvasController;
        private ISettingsUICanvasController settingsUICanvasController;
        
        public StartMainMenuStateCommand SetEnterData(MainMenuInitiatorEnterData mainMenuInitiatorEnterData)
        {
            this.mainMenuInitiatorEnterData = mainMenuInitiatorEnterData;
            return this;
        }
        
        public override void ResolveDependencies()
        {
            mainMenuUIGroupController = diContainer.Resolve<IMainMenuUIGroupController>();
            mainUICanvasController = diContainer.Resolve<IMainUICanvasController>();
            settingsUICanvasController = diContainer.Resolve<ISettingsUICanvasController>();
        }
        
        public Awaitable Execute(CancellationTokenSource cancellationTokenSource)
        {
            mainMenuUIGroupController.StartEntryPoint();
            mainUICanvasController.StartEntryPoint();
            settingsUICanvasController.StartEntryPoint();
            return AwaitableUtils.CompletedTask;
        }
    }
}
