using Ecs.Components;
using Ecs.Providers;
using ExtensionsMain;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class TargetSelectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter _towersFilter = Startup.World.Filter<TowerBase>().Exc<HasTarget>().End();
        private readonly EcsFilter _monstersFilter = Startup.World.Filter<Hitable>().End();
        private readonly EcsPool<Hitable> _hitablePool = Startup.World.GetPool<Hitable>();

        public void Run(EcsSystems systems)
        {
            foreach (var towerEntity in _towersFilter)
            {
                var tower = Startup.World.GetPool<TowerBase>().Get(towerEntity);

                foreach (var hitableEntity in _monstersFilter)
                {
                    var hitableTransform = _hitablePool.Get(hitableEntity).Transform;
                    if (hitableTransform.DistanceTo(tower.SelfTransform.position) > tower.Stats.Range) continue;

                    ref var hasTargetComponent = ref Startup.World.GetPool<HasTarget>().Add(towerEntity);
                    hasTargetComponent.Target = Startup.World.PackEntity(hitableEntity);
                    break;
                }
            }
        }
    }
}