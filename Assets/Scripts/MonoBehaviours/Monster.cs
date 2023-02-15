using Ecs;
using Ecs.Components;
using Leopotam.EcsLite;
using ScriptableObjects;
using UnityEngine;

namespace MonoBehaviours
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [field: SerializeField] public EcsPackedEntity Entity { get; private set; }
        [field: SerializeField] public MonsterStats Stats { get; private set; }


        private void OnEnable()
        {
            var entity = Startup.World.NewEntity();

            ref var movable = ref Startup.World.GetPool<MovableComponent>().Add(entity);
            movable.Speed = Stats.MoveSpeed;
            movable.Rigidbody = _rigidbody;

            ref var hitable = ref Startup.World.GetPool<HitableComponent>().Add(entity);
            hitable.MaxHealth = Stats.MaxHealth;
            hitable.CurrentHealth = hitable.MaxHealth;
            hitable.Provider = transform;

            Entity = Startup.World.PackEntity(entity);
        }
    }
}