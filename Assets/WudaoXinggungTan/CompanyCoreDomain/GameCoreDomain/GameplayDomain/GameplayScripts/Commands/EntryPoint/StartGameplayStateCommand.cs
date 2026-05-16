using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.CommandFactoryService.Commands;
using CompanyCoreScripts.Utils;
using GameplayScripts.MVC.UI;
using GameplayScripts.MVC.UI.HUD;
using GameCoreScripts._GameplayEnterData;

namespace GameplayScripts.Commands.EntryPoint
{
    public class StartGameplayStateCommand : BaseCommand, ICommandAsync
    {
        #region Dependencies

        private GameplayInitiatorEnterData gameplayInitiatorEnterData;
        private IGameplayUIGroupController gameplayUIGroupController;
        private IHudUICanvasController hudUICanvasController;

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
        }

        public Awaitable Execute(CancellationTokenSource cancellationTokenSource)
        {
            gameplayUIGroupController.StartEntryPoint();
            hudUICanvasController.StartEntryPoint();
            return AwaitableUtils.CompletedTask;        
        }

        #endregion
    }
}