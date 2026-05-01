using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.SceneInitiatorsService;
using Zenject;

namespace CompanyCoreScripts.Services.GameStateService
{
    public interface IGameState
    {
        CancellationTokenSource CancellationTokenSource { get; }
        GameStateType GameStateType { get; }
        Awaitable LoadState(CancellationTokenSource cancellationTokenSource);
        Awaitable StartState(CancellationTokenSource cancellationTokenSource);
        Awaitable ExitState(CancellationTokenSource cancellationTokenSource);
    }
}
