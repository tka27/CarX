using UnityEngine;

namespace SubLib.Extensions
{
    public static class ExtensionsEditor
    {
        public static void AutoSmooth(this AnimationCurve curve)
        {
#if UNITY_EDITOR
            for (int i = 0; i < curve.keys.Length; i++)
            {
                UnityEditor.AnimationUtility.SetKeyLeftTangentMode(curve, i,
                    UnityEditor.AnimationUtility.TangentMode.ClampedAuto);
                UnityEditor.AnimationUtility.SetKeyRightTangentMode(curve, i,
                    UnityEditor.AnimationUtility.TangentMode.ClampedAuto);
            }
#endif
        }

        public static void ClearCurve(this AnimationCurve curve)
        {
            for (int i = curve.keys.Length - 1; i >= 0; i--)
            {
                curve.RemoveKey(i);
            }
        }
    }
}