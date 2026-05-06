using System.Threading;
using UnityEngine;

namespace CompanyCoreScripts.MVC.LogoScreen
{
    public interface ILogoScreenController
    {
        public Awaitable Show(CancellationTokenSource cancellationTokenSource);
        public Awaitable Hide(CancellationTokenSource cancellationTokenSource);
    }
}