using System.Threading;
using UnityEngine;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.InitiatorInvokerService;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.LoggerService;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Utils;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.StateMachineService
{
    public abstract class BaseGameState<T> : IGameState where T : class, IInitiatorEnterData
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        public T EnterData { get; }

        protected BaseGameState(T enterData)
        {
            EnterData = enterData;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public CancellationTokenSource CancellationTokenSource => CancellationTokenSource.CreateLinkedTokenSource(_cancellationTokenSource.Token);
        public abstract GameStateType GameStateType { get; }

        public virtual Awaitable LoadState(CancellationTokenSource cancellationTokenSource)
        {
            MyLogService.LogTopic($"Load state {GameStateType}", LogTopicType.GameState);
            return AwaitableUtils.CompletedTask;
        }
        
        public virtual Awaitable StartState(CancellationTokenSource cancellationTokenSource)
        {
            MyLogService.LogTopic($"Start state {GameStateType}", LogTopicType.GameState);
            return AwaitableUtils.CompletedTask;
        }

        public virtual Awaitable ExitState(CancellationTokenSource cancellationTokenSource)
        {
            _cancellationTokenSource.Cancel();
            return AwaitableUtils.CompletedTask;
        }
    }
}