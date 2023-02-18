using MonoBehaviours;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class AimingResults : ScriptableObject
    {
        [field: SerializeField] public CannonProjectile Projectile { get; private set; }
        public AnimationCurve VerticalAngleCurve;
        public float GetVerticalAngle(float distanceToTarget) => VerticalAngleCurve.Evaluate(distanceToTarget);
    }
}