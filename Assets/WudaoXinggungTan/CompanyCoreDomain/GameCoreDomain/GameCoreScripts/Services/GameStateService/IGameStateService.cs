using System.Threading;
using UnityEngine;

namespace GameCoreScripts.Services.GameStateService
{
    public interface IGameStateService
    {
        public IGameState CurrentState();
        public Awaitable EnterInitialGameState(IGameState initialState ,CancellationTokenSource cancellationTokenSource);
        public void SwitchState(IGameState newState);
    }
}
