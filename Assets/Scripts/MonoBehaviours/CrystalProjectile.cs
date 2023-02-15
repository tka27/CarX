using Cysharp.Threading.Tasks;
using SubLib.Extensions;
using UnityEngine;

namespace MonoBehaviours
{
    public class CrystalProjectile : Projectile
    {
        [SerializeField] private float _speed;

        private SubLib.Async.ReusableCancellationTokenSource _cts;

        private void Awake()
        {
            _cts = new(this.GetCancellationTokenOnDestroy());
        }

        private void OnEnable()
        {
            _cts.Create();
        }

        private void OnDisable()
        {
            _cts.Cancel();
        }

        public void Fire(Transform target)
        {
            transform.MoveAsync(target, _speed, true, _cts.Token).Forget();
        }
    }
}