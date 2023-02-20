using UnityEngine;

public static class InterceptCalculator
{
    public static Vector3 GetInterceptPoint(Transform shooter, Rigidbody target,
        float projectileSpeed)
    {
        Vector3 relativePosition = target.position - shooter.position;
        Vector3 relativeVelocity = target.velocity;
        float timeToIntercept = FirstOrderInterceptTime(projectileSpeed, relativePosition, relativeVelocity);
        return target.position + timeToIntercept * (relativeVelocity);
    }

    private static float FirstOrderInterceptTime(float shotSpeed, Vector3 relativePosition, Vector3 relativeVelocity)
    {
        float a = Vector3.Dot(relativeVelocity, relativeVelocity) - shotSpeed * shotSpeed;
        float b = 2f * Vector3.Dot(relativeVelocity, relativePosition);
        float c = Vector3.Dot(relativePosition, relativePosition);
        float determinant = b * b - 4f * a * c;
        if (determinant > 0f)
        {
            float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a);
            float t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
            if (t1 > 0f) return t2 > 0f ? Mathf.Min(t1, t2) : t1;
            if (t2 > 0f) return t2;
        }

        return -1f;
    }
}