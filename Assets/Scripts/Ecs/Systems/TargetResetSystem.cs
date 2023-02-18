using Ecs.Components;
using Ecs.Providers;
using ExtensionsMain;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class TargetResetSystem : IEcsRunSystem
    {
        private readonly EcsFilter _towerFilter = Startup.World.Filter<HasTarget>().Inc<TowerBase>().End();

        private readonly EcsPool<HasTarget> _hasTargetPool = Startup.World.GetPool<HasTarget>();
        private readonly EcsPool<TowerBase> _towerPool = Startup.World.GetPool<TowerBase>();
        private readonly EcsPool<Hitable> _hitablePool = Startup.World.GetPool<Hitable>();


        public void Run(EcsSystems systems)
        {
            foreach (var towerEntity in _towerFilter)
            {
                if (!_hasTargetPool.Get(towerEntity).Target.Unpack(Startup.World, out var targetEntity))
                {
                    _hasTargetPool.Del(towerEntity);
                    continue;
                }

                ref var tower = ref _towerPool.Get(towerEntity);
                float distance = tower.SelfTransform.DistanceTo(_hitablePool.Get(targetEntity).Transform.position);

                if (distance > tower.Stats.Range) _hasTargetPool.Del(towerEntity);
            }
        }
    }
}