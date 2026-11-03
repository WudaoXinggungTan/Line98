using CompanyCoreScripts.Services.CommandFactoryService.Commands;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using GameCoreScripts.Services.GameStateService;
using GameCoreScripts._MainMenuEnterData;
using GameCoreScripts._MainMenuState;

namespace GameplayScripts.Commands.GameOver
{
    // For now losing simply returns to the main menu; a game over screen can be shown from here later
    public class GameOverCommand : BaseCommand, ICommandVoid
    {
        #region Dependencies

        private IGameStateService gameStateService;
        private MainMenuStateFactory mainMenuStateFactory;

        #endregion

        #region Public Methods

        public override void ResolveDependencies()
        {
            gameStateService = diContainer.Resolve<IGameStateService>();
            mainMenuStateFactory = diContainer.Resolve<MainMenuStateFactory>();
        }

        public void Execute()
        {
            MyLoggerService.Log("Game over: the board is full, back to the main menu");
            gameStateService.SwitchState(mainMenuStateFactory.Create(new MainMenuInitiatorEnterData()));
        }

        #endregion
    }
}
