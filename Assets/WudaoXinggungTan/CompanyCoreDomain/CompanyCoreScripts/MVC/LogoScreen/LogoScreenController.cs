using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Utils;

namespace CompanyCoreScripts.MVC.LogoScreen
{
    public class LogoScreenController : ILogoScreenController
    {
        #region Dependency

        private readonly LogoScreenView logoScreenView;

        #endregion

        #region Constructor

        public LogoScreenController(LogoScreenView logoScreenView)
        {
            this.logoScreenView = logoScreenView;
        }

        #endregion

        #region Public Methods
        
        public async Awaitable Show(CancellationTokenSource cancellationTokenSource)
        {
            await logoScreenView.Show(cancellationTokenSource);
        }

        public Awaitable Hide(CancellationTokenSource cancellationTokenSource)
        {
            logoScreenView.Hide();
            return AwaitableUtils.CompletedTask;
        }
        
        #endregion
        
    }
}