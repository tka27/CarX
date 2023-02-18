using Data;
using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class CannonTowerAttackSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _towerFilter;

        public void Init(EcsSystems systems)
        {
            _towerFilter = Startup.World.Filter<CannonTowerComponent>().Inc<HasTargetComponent>()
                .Exc<CooldownComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var cannonEntity in _towerFilter)
            {
                var cannonPool = Startup.World.GetPool<CannonTowerComponent>();
                var cooldownPool = Startup.World.GetPool<CooldownComponent>();
                var towerPool = Startup.World.GetPool<TowerBaseComponent>();

                ref var cannonTower = ref cannonPool.Get(cannonEntity);
                if (!cannonTower.Aimed) continue;

                PoolContainer.Instance.CannonProjectiles.Get().Shoot(cannonTower.VerticalAimTransform);

                cooldownPool.Add(cannonEntity).TimeLeft = towerPool.Get(cannonEntity).Stats.AttackCooldown;
            }
        }
    }
}