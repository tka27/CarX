using Ecs;
using Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace MonoBehaviours
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private int _damage = 50;

        private void OnTriggerEnter(Collider other)
        {
            if (!TryGetComponent<Monster>(out var monster)) return;

            monster.Entity.Unpack(Startup.World, out var entity);
            Startup.World.GetPool<DamageRequest>().Add(entity).Damage = _damage;
            gameObject.SetActive(false);
        }
    }
}