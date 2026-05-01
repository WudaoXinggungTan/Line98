using System.Threading;
using UnityEngine;

namespace CompanyCoreScripts.Services.CommandFactoryService.Commands
{
    public interface ICommandAsync : IBaseCommand
    {
        Awaitable Execute(CancellationTokenSource cancellationTokenSource);
    }
}
