using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class LightningProjectilesFollowSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter = Startup.World.Filter<LightningProjectile>().Inc<HasTarget>().End();

        private readonly EcsPool<HasTarget> _hasTargetPool = Startup.World.GetPool<HasTarget>();
        private readonly EcsPool<LightningProjectile> _projectilesPool = Startup.World.GetPool<LightningProjectile>();
        private readonly EcsPool<Hitable> _hitablePool = Startup.World.GetPool<Hitable>();

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (!_hasTargetPool.Get(entity).Target.Unpack(Startup.World, out var targetEntity)) continue;
                var targetTransform = _hitablePool.Get(targetEntity).Transform;
                ref var projectile = ref _projectilesPool.Get(entity);


                var projectilePosition = projectile.Transform.position;
                var velocity = (targetTransform.position - projectilePosition).normalized * projectile.Speed;

                projectilePosition += velocity;
                projectile.Transform.position = projectilePosition;
            }
        }
    }
}