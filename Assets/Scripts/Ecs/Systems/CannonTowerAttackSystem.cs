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
            _towerFilter = Startup.World.Filter<CannonTower>().Inc<HasTarget>()
                .Exc<CooldownComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var cannonEntity in _towerFilter)
            {
                var cannonPool = Startup.World.GetPool<CannonTower>();
                var cooldownPool = Startup.World.GetPool<CooldownComponent>();
                var towerPool = Startup.World.GetPool<TowerBaseComponent>();

                ref var cannonTower = ref cannonPool.Get(cannonEntity);
                if (!cannonTower.Aimed) continue;

                ref var towerBase = ref towerPool.Get(cannonEntity);
                PoolContainer.Instance.CannonProjectiles.Get().Shoot(towerBase.ShootPoint);
                cooldownPool.Add(cannonEntity).TimeLeft = towerBase.Stats.AttackCooldown;
            }
        }
    }
}