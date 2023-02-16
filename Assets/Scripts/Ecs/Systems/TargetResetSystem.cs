using Ecs.Components;
using ExtensionsMain;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class TargetResetSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _towerFilter;

        public void Init(EcsSystems systems)
        {
            _towerFilter = Startup.World.Filter<HasTargetComponent>().Inc<TowerBaseComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            var hasTargetPool = Startup.World.GetPool<HasTargetComponent>();
            var towerPool = Startup.World.GetPool<TowerBaseComponent>();
            var hitablePool = Startup.World.GetPool<HitableComponent>();

            foreach (var towerEntity in _towerFilter)
            {
                if (!hasTargetPool.Get(towerEntity).Target.Unpack(Startup.World, out var targetEntity))
                {
                    hasTargetPool.Del(towerEntity);
                    continue;
                }

                ref var tower = ref towerPool.Get(towerEntity);
                ref var hitable = ref hitablePool.Get(targetEntity);
                float distance = tower.SelfTransform.DistanceTo(hitable.Transform.position);

                if (distance > tower.Stats.Range) hasTargetPool.Del(towerEntity);
            }
        }
    }
}