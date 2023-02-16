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

        void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Monster>(out var monster)) return;
            gameObject.SetActive(false);

            monster.Entity.Unpack(Startup.World, out var entity);
            var pool = Startup.World.GetPool<DamageRequest>();
            if (pool.Has(entity)) pool.Get(entity).Damage += _damage;
            else pool.Add(entity).Damage = _damage;
        }
    }
}