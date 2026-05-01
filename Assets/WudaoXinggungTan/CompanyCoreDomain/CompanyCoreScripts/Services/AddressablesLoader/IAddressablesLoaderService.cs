using System.Threading;
using UnityEngine;

namespace CompanyCoreScripts.Services.AddressablesLoader
{
    public interface IAddressablesLoaderService
    {
        public Awaitable<T> LoadAsync<T>(string address, CancellationTokenSource cancellationTokenSource) where T : Object;
        public bool IsLoaded(string address);
        public void Release(string address);
        public void ReleaseAll();
    }
}
