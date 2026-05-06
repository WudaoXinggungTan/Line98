using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.CommandFactoryService.Commands;
using CompanyCoreScripts.Utils;
using GameCoreScripts._MainMenuEnterData;
using MainMenuScripts.MVC.UI;
using MainMenuScripts.MVC.UI.MainCanvas;
using MainMenuScripts.MVC.UI.SettingsCanvas;

namespace MainMenuScripts.Commands.EntryPoint
{
    public class LoadMainMenuStateCommand : BaseCommand, ICommandAsync
    {
        private MainMenuInitiatorEnterData mainMenuInitiatorEnterData;
        private IMainMenuUIGroupController mainMenuUIGroupController;
        private IMainUICanvasController mainUICanvasController;
        private ISettingsUICanvasController settingsUICanvasController;
        
        public LoadMainMenuStateCommand SetEnterData(MainMenuInitiatorEnterData mainMenuInitiatorEnterData)
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
            mainMenuUIGroupController.InitEntryPoint();
            mainUICanvasController.InitEntryPoint();
            settingsUICanvasController.InitEntryPoint();
            return AwaitableUtils.CompletedTask;
        }
    }
}
