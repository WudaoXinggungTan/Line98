using System;
using System.Threading;
using UnityEngine;
using Zenject;
using WudaoXinggungTan.CompanyCoreDomain.Plugins.InputSystem;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.LoggerService;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.CompanyCoreInitiator
{
    public class CompanyCoreInitiator : MonoBehaviour
    {
        private InputSystem_Actions inputSystemActions;
        private CustomLogger customLogger;

        [Inject] // The first entry point of the company core domain
        private void Setup(InputSystem_Actions inputSystemActions, CustomLogger customLogger)
        {
            this.inputSystemActions = inputSystemActions;
            this.customLogger = customLogger;
        }

        private void Start()
        {
            InitEntryPoint(CancellationTokenSource.CreateLinkedTokenSource(Application.exitCancellationToken));
        }

        private void InitEntryPoint(CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                EnableServices();
                LoadGameCoreScene(cancellationTokenSource);
            }
            catch (OperationCanceledException)
            {
                customLogger.Log("Operation init core was cancelled");
            }
            catch (Exception exception)
            {
                customLogger.LogError(exception.Message);
                throw;
            }
        }

        private void EnableServices()
        {
            inputSystemActions.Enable();
            customLogger.Enable();
        }

        private void LoadGameCoreScene(CancellationTokenSource cancellationTokenSource)
        {
        }
    }
}