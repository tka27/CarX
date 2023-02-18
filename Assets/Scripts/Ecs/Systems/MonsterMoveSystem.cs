using Data;
using Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Systems
{
    public sealed class MonsterMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter _movableFilter = Startup.World.Filter<Movable>().End();
        private readonly EcsPool<Movable> _movablePool = Startup.World.GetPool<Movable>();

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _movableFilter)
            {
                var movable = _movablePool.Get(entity);
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