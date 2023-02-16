using System;
using UnityEngine;

namespace Aiming
{
    public class AimingProjectile : MonoBehaviour
    {
        public event Action OnLandedEvent;
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public float StartSpeed { get; private set; }

        public void Shoot(Transform from)
        {
            transform.position = from.position;
            Rigidbody.velocity = from.forward * StartSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            OnLandedEvent?.Invoke();
        }
    }
}