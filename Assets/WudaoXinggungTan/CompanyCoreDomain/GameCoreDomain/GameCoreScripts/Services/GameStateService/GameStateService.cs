using System;
using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using GameCoreScripts.MVC.LoadingScreen;

namespace GameCoreScripts.Services.GameStateService
{
    public class GameStateService : IGameStateService
    {
        #region Dependencies

        private readonly ILoadingScreenController loadingScreenController;

        #endregion

        #region Dependencies

        private IGameState currentGameState;

        #endregion

        #region Constructor

        public GameStateService(ILoadingScreenController loadingScreenController)
        {
            this.loadingScreenController = loadingScreenController;
        }

        #endregion

        #region Private Methods

        private async Awaitable SwitchStateAsync(IGameState newState)
        {
            try
            {
                var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(Application.exitCancellationToken);

                if (currentGameState == null)
                {
                    MyLoggerService.LogError("No state to switch from, need to initialize a game state first!");
                    return;
                }

                loadingScreenController.Show();
                _ = loadingScreenController.SetLoadingSlider(0.5f, cancellationTokenSource);

                await currentGameState.ExitState(cancellationTokenSource);
                currentGameState = newState;
                await currentGameState.LoadState(cancellationTokenSource);

                await loadingScreenController.SetLoadingSlider(1, cancellationTokenSource);
                loadingScreenController.Hide();
                
                await currentGameState.StartState(cancellationTokenSource);
            }
            catch (OperationCanceledException)
            {
                MyLoggerService.Log("Switching state operation was cancelled");
            }
            catch (Exception exception)
            {
                MyLoggerService.LogException(exception);
                throw;
            }
        }

        #endregion
        
        #region Public Methods

        public IGameState CurrentState()
        {
            return currentGameState;
        }

        public async Awaitable EnterInitialGameState(IGameState initialState, CancellationTokenSource cancellationTokenSource)
        {
            currentGameState = initialState;

            loadingScreenController.Show();
            await loadingScreenController.ShowTransition(cancellationTokenSource);
            _ = loadingScreenController.SetLoadingSlider(0.5f, cancellationTokenSource);
            await currentGameState.LoadState(cancellationTokenSource);
            
            await loadingScreenController.SetLoadingSlider(1, cancellationTokenSource);
            loadingScreenController.Hide();
            
            await currentGameState.StartState(cancellationTokenSource);
        }

        public void SwitchState(IGameState newState)
        {
            _ = SwitchStateAsync(newState);
        }

        #endregion
    }
}
