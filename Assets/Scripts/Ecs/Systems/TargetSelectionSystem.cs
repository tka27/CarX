using Ecs.Components;
using ExtensionsMain;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class TargetSelectionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _towersFilter;
        private EcsFilter _monstersFilter;

        public void Init(EcsSystems systems)
        {
            _towersFilter = Startup.World.Filter<TowerComponent>().Exc<HasTargetComponent>().End();
            _monstersFilter = Startup.World.Filter<HitableComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var towerEntity in _towersFilter)
            {
                var tower = Startup.World.GetPool<TowerComponent>().Get(towerEntity);

                foreach (var hitableEntity in _monstersFilter)
                {
                    var hitable = Startup.World.GetPool<HitableComponent>().Get(hitableEntity);
                    if (hitable.Provider.DistanceTo(tower.Provider.position) > tower.Range) continue;

                    ref var hasTargetComponent = ref Startup.World.GetPool<HasTargetComponent>().Add(towerEntity);
                    hasTargetComponent.Target = hitable;
                }
            }
        }
    }
}