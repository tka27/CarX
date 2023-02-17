using UnityEditor;
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


        public float GetTimeToTarget(float distanceToTarget) => TimeCurve.Evaluate(distanceToTarget);
        public float GetVerticalAngle(float distanceToTarget) => VerticalAngleCurve.Evaluate(distanceToTarget);

        public void Clear()
        {
            ClearCurve(TimeCurve);
            ClearCurve(VerticalAngleCurve);
        }

        public void FinishResults()
        {
            Smooth(TimeCurve);
            Smooth(VerticalAngleCurve);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
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