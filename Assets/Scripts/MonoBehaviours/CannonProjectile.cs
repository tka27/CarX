using ExtensionsMain;
using UnityEngine;

namespace MonoBehaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class CannonProjectile : Projectile
    {
        [SerializeField, HideInInspector] private Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody;

        public void Shoot(Transform from)
        {
            transform.position = from.position;
            Rigidbody.velocity = from.forward * StartSpeed;
        }

        private void OnValidate()
        {
            gameObject.TrySetComponent(ref _rigidbody);
        }
    }
}