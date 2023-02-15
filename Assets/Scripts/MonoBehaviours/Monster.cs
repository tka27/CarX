using Ecs;
using Ecs.Components;
using Leopotam.EcsLite;
using ScriptableObjects;
using UnityEngine;

namespace MonoBehaviours
{
    public class Monster : MonoBehaviour
    {
        [field: SerializeField] public EcsPackedEntity Entity { get; private set; }
        [field: SerializeField] public MonsterStats Stats { get; private set; }


        private void OnEnable()
        {
            var entity = Startup.World.NewEntity();
            Startup.World.GetPool<MovableComponent>().Add(entity).Speed = Stats.MoveSpeed;
            var hitable = Startup.World.GetPool<HitableComponent>().Add(entity);
            hitable.MaxHealth = Stats.MaxHealth;
            hitable.CurrentHealth = hitable.MaxHealth;
            hitable.Provider = gameObject;
            Entity = Startup.World.PackEntity(entity);
        }
    }
}