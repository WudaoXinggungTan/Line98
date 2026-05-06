using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.LoggerService;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using CompanyCoreScripts.Services.UpdateService;

namespace CompanyCoreScripts.MVC.WorldCamera
{
    public class WorldCameraController : IWorldCameraController, IUpdatable
    {
        #region Dependencies

        private readonly WorldCameraView worldCameraView;
        private readonly IUpdateSubscriptionService updateSubscriptionService;

        #endregion

        #region Dependencies

        private Transform followTarget;

        #endregion

        #region Constructor

        public WorldCameraController(WorldCameraView worldCameraView, IUpdateSubscriptionService updateSubscriptionService)
        {
            this.worldCameraView = worldCameraView;
            this.updateSubscriptionService = updateSubscriptionService;
        }

        #endregion

        #region Private Methods

        private void LerpCameraRelativeToTarget()
        {
            worldCameraView.LerpPositionRelativeToTarget(followTarget);
            worldCameraView.LookAtTarget(followTarget);
        }

        private void SetCameraRelativeToTarget(Transform target)
        {
            worldCameraView.SetPositionRelativeToTarget(target);
            worldCameraView.LookAtTarget(target);
        }

        #endregion

        #region Public Methods

        public void StartFollowTarget(Transform targetTransform)
        {
            MyLoggerService.LogTopic($"Start follow target {targetTransform.gameObject.name}", LogTopicType.Camera);
            followTarget = targetTransform;
            SetCameraRelativeToTarget(followTarget);
            updateSubscriptionService.RegisterUpdatable(this);
        }

        public async Awaitable DoLockOnTargetAnimation(Transform targetTransform, CancellationTokenSource cancellationTokenSource)
        {
            MyLoggerService.LogTopic($"Do lock on target animation {targetTransform.gameObject.name}", LogTopicType.Camera);
            SetCameraRelativeToTarget(targetTransform);
            await worldCameraView.DoLockOnTargetAnimation(worldCameraView.transform.position, worldCameraView.transform.rotation.eulerAngles, cancellationTokenSource);
        }

        public void StopFollowTarget()
        {
            MyLoggerService.LogTopic("Stop follow target", LogTopicType.Camera);
            updateSubscriptionService.UnregisterUpdatable(this);
            followTarget = null;
        }

        public void ManagedUpdate()
        {
            LerpCameraRelativeToTarget();
        }

        #endregion
    }
}
