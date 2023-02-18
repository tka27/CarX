using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class LightningProjectilesLifeControlSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter = Startup.World.Filter<LightningProjectile>().Inc<HasTarget>().End();
        private readonly EcsPool<HasTarget> _hasTargetPool = Startup.World.GetPool<HasTarget>();

        private readonly EcsPool<LightningProjectile> _lightningProjectilePool =
            Startup.World.GetPool<LightningProjectile>();

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (_hasTargetPool.Get(entity).Target.Unpack(Startup.World, out _)) continue;

                _lightningProjectilePool.Get(entity).Transform.gameObject.SetActive(false);
            }
        }
    }
}