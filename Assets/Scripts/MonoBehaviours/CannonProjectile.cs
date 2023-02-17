using ExtensionsMain;
using UnityEngine;

namespace MonoBehaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class CannonProjectile : Projectile
    {
        [SerializeField, HideInInspector] private Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody;

        private void OnValidate()
        {
            gameObject.TrySetComponent(ref _rigidbody);
        }
    }
}