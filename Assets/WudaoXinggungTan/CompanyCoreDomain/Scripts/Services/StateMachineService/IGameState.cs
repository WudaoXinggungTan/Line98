using System.Threading;
using UnityEngine;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.StateMachineService
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