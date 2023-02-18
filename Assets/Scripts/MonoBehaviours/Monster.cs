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

            ref var movable = ref Startup.World.GetPool<Movable>().Add(entity);
            movable.Speed = Stats.MoveSpeed;
            movable.Rigidbody = _rigidbody;

            ref var hitable = ref Startup.World.GetPool<Hitable>().Add(entity);
            hitable.CurrentHealth = Stats.MaxHealth;
            hitable.Transform = transform;

            Entity = Startup.World.PackEntity(entity);
        }
    }
}