using System.Threading;
using UnityEngine;

namespace GameCoreScripts.Services.GameStateService
{
    public interface IGameState
    {
        public CancellationTokenSource CancellationTokenSource { get; }
        public GameStateType GameStateType { get; }
        public Awaitable LoadState(CancellationTokenSource cancellationTokenSource);
        public Awaitable StartState(CancellationTokenSource cancellationTokenSource);
        public Awaitable ExitState(CancellationTokenSource cancellationTokenSource);
    }
}
