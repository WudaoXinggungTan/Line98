using System.Threading;
using UnityEngine;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Helpers.ObjectPoolsHelpers
{
    public interface IPoolAsync<T> where T : IPoolable
    {
        Awaitable InitPool(CancellationTokenSource cancellationTokenSource);
        Awaitable<T> Spawn(CancellationTokenSource cancellationTokenSource);
    }
}