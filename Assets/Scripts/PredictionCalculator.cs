using System;
using UnityEngine;

public static class PredictionCalculator
{
    // public static Vector3 GetPredictionPoint(Transform shooter, Rigidbody target,
    //     float projectileSpeed)
    // {
    //     var speedRatio = target.velocity.magnitude / projectileSpeed;
    //     return target.position +
    //            target.velocity.normalized * speedRatio * (shooter.position - target.position).magnitude;
    // }

    public static Vector3 GetPredictionPoint(Transform shooter, Rigidbody target,
        float projectileSpeed)
    {
        var lookVector = shooter.position - target.position;
        float speedRatio = target.velocity.magnitude / projectileSpeed;

        float angleB = Vector3.Angle(lookVector, target.velocity);

        float deltaAngle = speedRatio * Mathf.Sin(angleB * Mathf.Deg2Rad);

        if (speedRatio > 1 && deltaAngle > 1) throw new Exception("Target is unreachable");
        float angleC = Mathf.Asin(deltaAngle) * Mathf.Rad2Deg;

        float angleA = 180f - angleB - angleC;

        float sideC = Mathf.Sin(angleA * Mathf.Deg2Rad) / Mathf.Sin(angleB * Mathf.Deg2Rad);


        float fromTargetToPrediction = lookVector.magnitude / sideC * speedRatio;
        return target.velocity.normalized * fromTargetToPrediction + target.position;
    }
}