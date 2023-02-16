using Data;
using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class LightningProjectilesFollowSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;

        public void Init(EcsSystems systems)
        {
            _filter = Startup.World.Filter<LightningProjectileComponent>().Inc<HasTargetComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            var hasTargetPool = Startup.World.GetPool<HasTargetComponent>();
            var projectilesPool = Startup.World.GetPool<LightningProjectileComponent>();
            var hitablePool = Startup.World.GetPool<HitableComponent>();
            foreach (var entity in _filter)
            {
                if (!hasTargetPool.Get(entity).Target.Unpack(Startup.World, out var targetEntity)) continue;
                var targetTransform = hitablePool.Get(targetEntity).Transform;
                var projectileTransform = projectilesPool.Get(entity).Transform;

                var projectilePosition = projectileTransform.position;
                var velocity = (targetTransform.position - projectilePosition).normalized *
                               LevelData.Instance.LightningProjectilesSpeed;

                projectilePosition += velocity;
                projectileTransform.position = projectilePosition;
            }
        }
    }
}