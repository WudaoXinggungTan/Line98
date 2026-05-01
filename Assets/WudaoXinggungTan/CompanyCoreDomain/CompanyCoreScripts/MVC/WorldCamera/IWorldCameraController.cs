using System.Threading;
using UnityEngine;

namespace CompanyCoreScripts.MVC.WorldCamera
{
    public interface IWorldCameraController
    {
        void StopFollowTarget();
        void StartFollowTarget(Transform targetTransform);
        Awaitable DoLockOnTargetAnimation(Transform targetTransform, CancellationTokenSource cancellationTokenSource);
    }
}
