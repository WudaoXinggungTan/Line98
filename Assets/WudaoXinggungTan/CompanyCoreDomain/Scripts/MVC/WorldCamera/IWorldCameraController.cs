using System.Threading;
using UnityEngine;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.MVC.WorldCamera
{
    public interface IWorldCameraController
    {
        void StopFollowTarget();
        void StartFollowTarget(Transform targetTransform);
        Awaitable DoLockOnTargetAnimation(Transform targetTransform, CancellationTokenSource cancellationTokenSource);
    }
}