using System.Threading;
using UnityEngine;

namespace CompanyCoreScripts.Services.CommandFactoryService.Commands
{
    public interface ICommandAsyncWithResult<TReturn> : IBaseCommand
    {
        Awaitable<TReturn> Execute(CancellationTokenSource cancellationTokenSource = null);
    }
}
