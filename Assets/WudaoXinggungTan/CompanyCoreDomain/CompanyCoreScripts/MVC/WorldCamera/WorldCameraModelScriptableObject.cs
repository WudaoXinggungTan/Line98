using UnityEngine;
using DG.Tweening;

namespace WudaoXinggungTan.CompanyCoreDomain.ReusableAssets.WorldCamera.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WorldCameraModelScriptableObject", menuName = "Scriptable Objects/WorldCameraModelScriptableObject")]
    public class WorldCameraModelScriptableObject : ScriptableObject
    {
        [Header("Focus On Target Settings")]
        public float distanceToTarget = 21.26f;
        public float distanceToTargetDamping = 4.0f;
        public float heightAboveTarget = 7.7f;
        public float heightAboveTargetDamping = 4.0f;
        public float viewRotationAngle = 43.4f;
        public float viewRotationDamping = 3.0f;
        public Vector3 offsetFromTarget = new Vector3(0, 0.85f, -1f);

        [Header("Focus On Target Animation Settings")]
        public float animationDurationInSeconds;
        public Vector3 startPosition;
        public Vector3 startRotationInEuler;
        public Ease animationEase;
    }
}
