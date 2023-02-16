using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class LightningProjectilesLifeControlSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;

        public void Init(EcsSystems systems)
        {
            _filter = Startup.World.Filter<LightningProjectileComponent>().Inc<HasTargetComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            var hasTargetPool = Startup.World.GetPool<HasTargetComponent>();
            var lightningProjectilePool = Startup.World.GetPool<LightningProjectileComponent>();

            foreach (var entity in _filter)
            {
                if (!hasTargetPool.Get(entity).Target.Unpack(Startup.World, out _)) continue;

                lightningProjectilePool.Get(entity).Transform.gameObject.SetActive(false);
            }
        }
    }
}