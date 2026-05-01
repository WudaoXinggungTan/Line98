
using System.Threading;
using UnityEngine;

namespace CompanyCoreScripts.Services.GameStateService
{
    public interface IGameStateService
    {
        IGameState CurrentState();
        Awaitable EnterInitialGameState(IGameState initialState ,CancellationTokenSource cancellationTokenSource);
        void SwitchState(IGameState newState);
    }
}
