using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class LightningProjectilesFollowSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;

        public void Init(EcsSystems systems)
        {
            _filter = Startup.World.Filter<LightningProjectile>().Inc<HasTarget>().End();
        }

        public void Run(EcsSystems systems)
        {
            var hasTargetPool = Startup.World.GetPool<HasTarget>();
            var projectilesPool = Startup.World.GetPool<LightningProjectile>();
            var hitablePool = Startup.World.GetPool<HitableComponent>();
            foreach (var entity in _filter)
            {
                if (!hasTargetPool.Get(entity).Target.Unpack(Startup.World, out var targetEntity)) continue;
                var targetTransform = hitablePool.Get(targetEntity).Transform;
                ref var projectile = ref projectilesPool.Get(entity);


                var projectilePosition = projectile.Transform.position;
                var velocity = (targetTransform.position - projectilePosition).normalized * projectile.Speed;

                projectilePosition += velocity;
                projectile.Transform.position = projectilePosition;
            }
        }
    }
}