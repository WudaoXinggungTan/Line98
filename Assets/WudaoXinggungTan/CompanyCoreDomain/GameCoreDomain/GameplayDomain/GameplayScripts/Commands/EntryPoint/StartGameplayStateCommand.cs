using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.CommandFactoryService.Commands;
using CompanyCoreScripts.Utils;
using GameplayScripts.MVC.Gameplay.Ball;
using GameplayScripts.MVC.Gameplay.Board;
using GameplayScripts.MVC.UI;
using GameplayScripts.MVC.UI.HUD;
using GameplayScripts.MVC.UI.Settings;
using GameCoreScripts._GameplayEnterData;

namespace GameplayScripts.Commands.EntryPoint
{
    public class StartGameplayStateCommand : BaseCommand, ICommandAsync
    {
        #region Dependencies

        private GameplayInitiatorEnterData gameplayInitiatorEnterData;
        private IGameplayUIGroupController gameplayUIGroupController;
        private IHudUICanvasController hudUICanvasController;
        private ISettingsUICanvasController settingsUICanvasController;
        private IBoardController boardController;
        private IBallController ballController;

        #endregion

        #region Constructor

        public StartGameplayStateCommand SetEnterData(GameplayInitiatorEnterData gameplayInitiatorEnterData)
        {
            this.gameplayInitiatorEnterData = gameplayInitiatorEnterData;
            return this;
        }

        #endregion

        #region Public Methods

        public override void ResolveDependencies()
        {
            gameplayUIGroupController = diContainer.Resolve<IGameplayUIGroupController>();
            hudUICanvasController = diContainer.Resolve<IHudUICanvasController>();
            settingsUICanvasController = diContainer.Resolve<ISettingsUICanvasController>();
            boardController = diContainer.Resolve<IBoardController>();
            ballController = diContainer.Resolve<IBallController>();
        }

        public Awaitable Execute(CancellationTokenSource cancellationTokenSource)
        {
            gameplayUIGroupController.StartEntryPoint();
            hudUICanvasController.StartEntryPoint();
            settingsUICanvasController.StartEntryPoint();
            boardController.StartEntryPoint();
            ballController.StartEntryPoint();
            return AwaitableUtils.CompletedTask;
        }

        #endregion
    }
}
