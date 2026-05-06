using System.Threading;
using DG.Tweening;
using UnityEngine;
using CompanyCoreScripts.Extensions;
using WudaoXinggungTan.CompanyCoreDomain.ReusableAssets.WorldCamera.ScriptableObjects;

namespace CompanyCoreScripts.MVC.WorldCamera
{
    public class WorldCameraView : MonoBehaviour
    {
        #region Dependencies

        [SerializeField] private WorldCameraModelScriptableObject worldCameraModelScriptableObject;

        private float currentDistanceToTarget;
        private string lockOnTargetAnimationClipName;

        #endregion

        #region Public Methods

        public void SetPositionRelativeToTarget(Transform target)
        {
            var wantedRotationAngle = worldCameraModelScriptableObject.viewRotationAngle;
            var wantedHeight = target.position.y + worldCameraModelScriptableObject.heightAboveTarget;
            var wantedDistance = worldCameraModelScriptableObject.distanceToTarget;

            currentDistanceToTarget = wantedDistance;
            var currentRotation = Quaternion.Euler(0, wantedRotationAngle, 0);
            var newPosition = target.position - currentRotation * Vector3.forward * currentDistanceToTarget;
            newPosition += worldCameraModelScriptableObject.offsetFromTarget;
            newPosition.y = wantedHeight;
            transform.position = newPosition;
        }

        public void LerpPositionRelativeToTarget(Transform target)
        {
            var wantedRotationAngle = worldCameraModelScriptableObject.viewRotationAngle;
            var wantedHeight = target.position.y + worldCameraModelScriptableObject.heightAboveTarget;
            var wantedDistance = worldCameraModelScriptableObject.distanceToTarget;
            var currentRotationAngle = transform.eulerAngles.y;
            var currentHeight = transform.position.y;

            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, worldCameraModelScriptableObject.viewRotationDamping * Time.deltaTime);
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, worldCameraModelScriptableObject.heightAboveTargetDamping * Time.deltaTime);
            currentDistanceToTarget = Mathf.Lerp(currentDistanceToTarget, wantedDistance, worldCameraModelScriptableObject.distanceToTargetDamping * Time.deltaTime);
            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
            var newPosition = target.position - currentRotation * Vector3.forward * currentDistanceToTarget;
            newPosition += worldCameraModelScriptableObject.offsetFromTarget;
            newPosition.y = currentHeight;
            transform.position = newPosition;
        }

        public void LookAtTarget(Transform target)
        {
            transform.LookAt(target.position + worldCameraModelScriptableObject.offsetFromTarget);
        }

        public async Awaitable DoLockOnTargetAnimation(Vector3 endPosition, Vector3 endRotationEuler, CancellationTokenSource cancellationTokenSource)
        {
            transform.position = worldCameraModelScriptableObject.startPosition;
            transform.rotation = Quaternion.Euler(worldCameraModelScriptableObject.startRotationInEuler);
            var seq = DOTween.Sequence();
            seq.Join(transform.DOMove(endPosition, worldCameraModelScriptableObject.animationDurationInSeconds).SetEase(worldCameraModelScriptableObject.animationEase));
            seq.Join(transform.DORotate(endRotationEuler, worldCameraModelScriptableObject.animationDurationInSeconds).SetEase(worldCameraModelScriptableObject.animationEase));
            await seq.WithCancellationSafe(cancellationTokenSource.Token);
        }

        #endregion
    }
}