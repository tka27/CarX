using System;
using UnityEngine;

namespace DefaultNamespace
{
    public static class PredictionCalculator
    {
        public static Vector3 GetPredictionPoint(Transform shooter, Rigidbody target,
            float projectileSpeed)
        {
            //Соотношение расстояний которое цель и пуля проходит за единицу времени
            float speedRelation = target.velocity.magnitude / projectileSpeed;

            float sideAGlobal; //расстояние от цели до точки перехвата (вичисляется позже)
            float sideA = speedRelation; //Относительное расстояние от цели до точки перехвата
            float sideB = 1f; //Относительное расстояние от стрелка до точки перехвата

            //вычисляем Расстояние от стрелка до цели
            float sideCGlobal = Vector3.Distance(shooter.position, target.position);

            float sideC; //Относительное расстояние от стрелка до цели (вичисляется позже)
            float angleA; //угол между вектором от стрелка до точки перехвата и вектором от цели до точки перехвата
            float angleB =
                Vector3.Angle(-(target.position - shooter.position), target.velocity); //Угол между стрелком и целью
            float angleC; //угол между вектором движения стрелка и вектором от стрелка до точки перехвата
            float deltaAngle = sideA / sideB * Mathf.Sin(angleB * Mathf.Deg2Rad);

            if (sideA > sideB && deltaAngle > 1) throw new Exception("Target is unreachable");
            //Вычисляем угол между вектором движения стрелка и вектором от стрелка до точки перехвата
            angleC = Mathf.Asin(deltaAngle) * Mathf.Rad2Deg;

            //Вычисляем угол между вектором от стрелка до точки перехвата и вектором от цели до точки перехвата
            angleA = 180f - angleB - angleC;

            //вычисляем тносительное расстояние от стрелка до цели
            sideC = Mathf.Sin(angleA * Mathf.Deg2Rad) / Mathf.Sin(angleB * Mathf.Deg2Rad);

            //Вычисляем расстояние от цели до точки перехвата
            sideAGlobal = sideCGlobal / sideC * sideA;

            //Вычисляем координаты точки перехвата
            Vector3 sP = target.velocity.normalized * sideAGlobal + target.position;
            return sP;
        }
    }
}