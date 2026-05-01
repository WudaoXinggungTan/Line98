using System.Threading;
using CompanyCoreScripts.Services.CommandFactoryService.Factory;
using CompanyCoreScripts.Services.SceneInitiatorsService;
using CompanyCoreScripts.Services.SceneLoaderService;
using CompanyCoreScripts.Utils;
using GameCoreScripts._MainMenuEnterData;
using MainMenuScripts.Commands.EntryPoint;
using UnityEngine;

namespace MainMenuScripts._MainMenuInitiator
{
    public class MainMenuInitiator : ISceneInitiator, IMainMenuInitiator
    {
        public SceneType SceneType => SceneType.MainMenuScene;

        private readonly ISceneInitiatorsService sceneInitiatorsService;
        private readonly ICommandFactory commandFactory;

        public MainMenuInitiator(ISceneInitiatorsService sceneInitiatorsService, ICommandFactory commandFactory)
        {
            this.sceneInitiatorsService = sceneInitiatorsService;
            this.sceneInitiatorsService.RegisterInitiator(this);

            this.commandFactory = commandFactory;
        }
        
        public async Awaitable LoadEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource)
        {
            MainMenuInitiatorEnterData mainMenuInitiatorEnterData = (MainMenuInitiatorEnterData)enterDataObject;
            await commandFactory.CreateCommandAsync<LoadMainMenuStateCommand>().SetEnterData(mainMenuInitiatorEnterData).Execute(cancellationTokenSource);
        }

        public async Awaitable StartEntryPoint(IInitiatorEnterData enterDataObject, CancellationTokenSource cancellationTokenSource)
        {
            MainMenuInitiatorEnterData mainMenuInitiatorEnterData = (MainMenuInitiatorEnterData)enterDataObject;
            await commandFactory.CreateCommandAsync<StartMainMenuStateCommand>().SetEnterData(mainMenuInitiatorEnterData).Execute(cancellationTokenSource);
        }

        public Awaitable InitExitPoint(CancellationTokenSource cancellationTokenSource)
        {
            sceneInitiatorsService.UnregisterInitiator(this);
            commandFactory.CreateCommandVoid<ExitMainMenuStateCommand>().Execute();
            return AwaitableUtils.CompletedTask;
        }
    }
}