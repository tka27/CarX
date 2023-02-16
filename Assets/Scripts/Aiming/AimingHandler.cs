using ScriptableObjects;
using UnityEngine;

namespace Aiming
{
    public class AimingHandler : MonoBehaviour
    {
        [SerializeField] private Transform _verticalAimingPart;
        [SerializeField] private AimingProjectile _projectile;

        [SerializeField] private float _angleStep = 5;


        [SerializeField] private AimingResults _results;

        private float _lastTime;
        private float _lastDistance;

        private void Start()
        {
            _results.Clear();
            _projectile.OnLandedEvent += OnProjectileLanded;
            _projectile.Shoot(_verticalAimingPart);
            _results.ProjectileMass = _projectile.Rigidbody.mass.ToString();
            _results.ProjectileStartSpeed = _projectile.StartSpeed.ToString();
        }

        private void OnDestroy()
        {
            _projectile.OnLandedEvent -= OnProjectileLanded;
        }

        private void Update()
        {
            _lastTime += Time.deltaTime;
        }

        private void OnProjectileLanded()
        {
            _lastDistance = (_projectile.transform.position - transform.position).magnitude;
            var currentAngle = _verticalAimingPart.localRotation.eulerAngles;

            float keyAngle = currentAngle.x;
            if (keyAngle > 45) keyAngle -= 360;
            var angleKeyframe = new Keyframe(_lastDistance, keyAngle);

            var timeKeyframe = new Keyframe(_lastDistance, _lastTime);

            _results.VerticalAngleCurve.AddKey(angleKeyframe);
            _results.TimeCurve.AddKey(timeKeyframe);


            currentAngle.x -= _angleStep;
            if (currentAngle.x is < 315 and > 30)
            {
                _results.FinishResults();
                return;
            }

            _verticalAimingPart.localRotation = Quaternion.Euler(currentAngle);

            _projectile.Shoot(_verticalAimingPart);
            _lastTime = 0;
        }
    }
}