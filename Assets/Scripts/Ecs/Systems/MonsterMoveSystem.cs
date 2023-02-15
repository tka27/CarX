using Ecs.Components;
using Game.Scripts.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Systems
{
    public sealed class MonsterMoveSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _movableFilter;

        public void Init(EcsSystems systems)
        {
            _movableFilter = Startup.World.Filter<MovableComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            var movablePool = Startup.World.GetPool<MovableComponent>();

            foreach (var entity in _movableFilter)
            {
                var movable = movablePool.Get(entity);
                var targetVector = LevelData.Instance.MonstersTarget.position - movable.Rigidbody.position;

                const float finishDistance = 0.5f;
                if (targetVector.magnitude < finishDistance)
                {
                    movable.Rigidbody.gameObject.SetActive(false);
                    Startup.World.DelEntity(entity);
                    continue;
                }

                movable.Rigidbody.velocity = targetVector.normalized * movable.Speed * Time.fixedDeltaTime;
            }
        }
    }
}