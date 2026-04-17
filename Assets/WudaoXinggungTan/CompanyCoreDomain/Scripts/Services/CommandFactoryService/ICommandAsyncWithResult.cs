using System.Threading;
using UnityEngine;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.CommandFactoryService
{
    public interface ICommandAsyncWithResult<TReturn> : IBaseCommand
    {
        Awaitable<TReturn> Execute(CancellationTokenSource cancellationTokenSource = null);
    }
}