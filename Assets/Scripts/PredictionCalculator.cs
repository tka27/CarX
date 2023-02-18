using System;
using UnityEngine;

public static class PredictionCalculator
{
    public static Vector3 GetPredictionPoint(Transform shooter, Rigidbody target,
        float projectileSpeed)
    {
        float speedRelation = target.velocity.magnitude / projectileSpeed;
    
        float sideA = speedRelation; //Относительное расстояние от цели до точки перехвата
        float sideB = 1f; //Относительное расстояние от стрелка до точки перехвата
    
    
        float distanceToTarget = (shooter.position - target.position).magnitude;
    
        float angleB =
            Vector3.Angle(-(target.position - shooter.position), target.velocity); //Угол между стрелком и целью
    
        float deltaAngle = sideA / sideB * Mathf.Sin(angleB * Mathf.Deg2Rad);
    
        if (sideA > sideB && deltaAngle > 1) throw new Exception("Target is unreachable");
        //Вычисляем угол между вектором движения стрелка и вектором от стрелка до точки перехвата
        float angleC = Mathf.Asin(deltaAngle) * Mathf.Rad2Deg;
    
        //Вычисляем угол между вектором от стрелка до точки перехвата и вектором от цели до точки перехвата
        float angleA = 180f - angleB - angleC;
    
        //вычисляем тносительное расстояние от стрелка до цели
        float sideC = Mathf.Sin(angleA * Mathf.Deg2Rad) / Mathf.Sin(angleB * Mathf.Deg2Rad);
    
    
        float fromTargetToPrediction = distanceToTarget / sideC * sideA;
        return target.velocity.normalized * fromTargetToPrediction + target.position;
    }
}