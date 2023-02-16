using UnityEngine;
using static UnityEditor.AnimationUtility;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class AimingResults : ScriptableObject
    {
        public string ProjectileMass;
        public string ProjectileStartSpeed;
        [Header("X - distance")] public AnimationCurve TimeCurve;
        public AnimationCurve VerticalAngleCurve;

        public void Clear()
        {
            ClearCurve(TimeCurve);
            ClearCurve(VerticalAngleCurve);
        }

        public void FinishResults()
        {
            Smooth(TimeCurve);
            Smooth(VerticalAngleCurve);
        }

        private void Smooth(AnimationCurve curve)
        {
            for (int i = curve.keys.Length - 1; i >= 0; i--)
            {
                SetKeyLeftTangentMode(curve, i, TangentMode.ClampedAuto);
                SetKeyRightTangentMode(curve, i, TangentMode.ClampedAuto);
            }
        }

        private void ClearCurve(AnimationCurve curve)
        {
            for (int i = curve.keys.Length - 1; i >= 0; i--)
            {
                curve.RemoveKey(i);
            }
        }
    }
}