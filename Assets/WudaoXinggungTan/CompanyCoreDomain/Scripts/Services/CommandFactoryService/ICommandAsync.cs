using System.Threading;
using UnityEngine;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.CommandFactoryService
{
    public interface ICommandAsync : IBaseCommand
    {
        Awaitable Execute(CancellationTokenSource cancellationTokenSource);
    }
}