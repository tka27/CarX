using Data;
using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class CannonTowerAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter _towerFilter =
            Startup.World.Filter<CannonTower>().Inc<HasTarget>().Exc<Cooldown>().End();

        private readonly EcsPool<CannonTower> _cannonPool = Startup.World.GetPool<CannonTower>();
        private readonly EcsPool<Cooldown> _cooldownPool = Startup.World.GetPool<Cooldown>();
        private readonly EcsPool<TowerBase> _towerPool = Startup.World.GetPool<TowerBase>();


        public void Run(EcsSystems systems)
        {
            foreach (var cannonEntity in _towerFilter)
            {
                ref var cannonTower = ref _cannonPool.Get(cannonEntity);
                if (!cannonTower.Aimed) continue;

                ref var towerBase = ref _towerPool.Get(cannonEntity);
                PoolContainer.Instance.CannonProjectiles.Get().Shoot(towerBase.ShootPoint);
                _cooldownPool.Add(cannonEntity).TimeLeft = towerBase.Stats.AttackCooldown;
            }
        }
    }
}