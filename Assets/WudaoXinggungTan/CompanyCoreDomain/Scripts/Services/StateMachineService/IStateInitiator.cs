using UnityEngine;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.InitiatorInvokerService;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.StateMachineService
{
    public interface IStateInitiator<T> where T : class, IInitiatorEnterData
    {
        Awaitable EnterState(T stateEnterData = null);
        Awaitable ExitState();
    }
}