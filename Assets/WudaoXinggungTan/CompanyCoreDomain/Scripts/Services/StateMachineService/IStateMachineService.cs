
using System.Threading;
using UnityEngine;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.StateMachineService
{
    public interface IStateMachineService
    {
        IGameState CurrentState();
        Awaitable EnterInitialGameState(IGameState initialState ,CancellationTokenSource cancellationTokenSource);
        void SwitchState(IGameState newState);
    }
}