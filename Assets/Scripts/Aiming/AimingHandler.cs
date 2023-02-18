using MonoBehaviours;
using ScriptableObjects;
using SubLib.Extensions;
using UnityEngine;

namespace Aiming
{
    public class AimingHandler : MonoBehaviour
    {
        [SerializeField] private Transform _verticalAimingPart;
        [SerializeField] private float _angleStep = 5;
        [SerializeField] private AimingResults _results;
        private CannonProjectile _aimingProjectile;
        private float _lastDistance;

        private void Start()
        {
            ClearResults();
            _aimingProjectile = Instantiate(_results.Projectile);
            _aimingProjectile.OnLandedEvent += OnProjectileLanded;
            _aimingProjectile.Shoot(_verticalAimingPart);
        }

        private void OnDestroy()
        {
            _aimingProjectile.OnLandedEvent -= OnProjectileLanded;
        }

        private void OnProjectileLanded()
        {
            _aimingProjectile.gameObject.SetActive(true);
            _lastDistance = (_aimingProjectile.transform.position - transform.position).magnitude;
            var currentAngle = _verticalAimingPart.localRotation.eulerAngles;

            float keyAngle = currentAngle.x;
            if (keyAngle > 45) keyAngle -= 360;
            var angleKeyframe = new Keyframe(_lastDistance, keyAngle);


            _results.VerticalAngleCurve.AddKey(angleKeyframe);


            currentAngle.x -= _angleStep;
            if (currentAngle.x is < 315 and > 30)
            {
                FinishResults();
                return;
            }

            _verticalAimingPart.localRotation = Quaternion.Euler(currentAngle);
            _aimingProjectile.Shoot(_verticalAimingPart);
        }


        private void ClearResults()
        {
            _results.VerticalAngleCurve.ClearCurve();
        }

        private void FinishResults()
        {
            _aimingProjectile.OnLandedEvent -= OnProjectileLanded;
            _results.VerticalAngleCurve.AutoSmooth();

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}