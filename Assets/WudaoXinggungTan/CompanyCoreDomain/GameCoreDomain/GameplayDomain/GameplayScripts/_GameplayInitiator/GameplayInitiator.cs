using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.CommandFactoryService.Factory;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using CompanyCoreScripts.Services.SceneInitiatorsService;
using CompanyCoreScripts.Services.SceneLoaderService;
using CompanyCoreScripts.Utils;
using GameplayScripts.Commands.EntryPoint;
using GameCoreScripts._GameplayEnterData;

namespace GameplayScripts._GameplayInitiator
{
    public class GameplayInitiator : ISceneInitiator, IGameplayInitiator
    {
        public SceneType SceneType => SceneType.GameplayScene;

        #region Dependencies

        private readonly ISceneInitiatorsService sceneInitiatorsService;
        private readonly ICommandFactory commandFactory;

        #endregion

        #region Constructor

        public GameplayInitiator(ISceneInitiatorsService sceneInitiatorsService, ICommandFactory commandFactory)
        {
            this.sceneInitiatorsService = sceneInitiatorsService;
            this.sceneInitiatorsService.RegisterInitiator(this);
            this.commandFactory = commandFactory;
        }

        #endregion

        #region Public Methods

        public async Awaitable LoadEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource)
        {
            GameplayInitiatorEnterData gameplayInitiatorEnterData = (GameplayInitiatorEnterData)enterDataObject;
            await commandFactory.CreateCommandAsync<LoadGameplayStateCommand>().SetEnterData(gameplayInitiatorEnterData).Execute(cancellationTokenSource);
        }

        public async Awaitable StartEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource)
        {
            GameplayInitiatorEnterData gameplayInitiatorEnterData = (GameplayInitiatorEnterData)enterDataObject;
            await commandFactory.CreateCommandAsync<StartGameplayStateCommand>().SetEnterData(gameplayInitiatorEnterData).Execute(cancellationTokenSource);
        }

        public Awaitable InitExitPoint(CancellationTokenSource cancellationTokenSource)
        {
            sceneInitiatorsService.UnregisterInitiator(this);
            commandFactory.CreateCommandVoid<ExitGameplayStateCommand>().Execute();
            return AwaitableUtils.CompletedTask;
        }

        #endregion
    }
}