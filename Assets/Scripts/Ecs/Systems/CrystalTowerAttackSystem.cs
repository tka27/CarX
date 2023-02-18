using Data;
using Ecs.Components;
using Ecs.Providers;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class CrystalTowerAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter _towersFilter = Startup.World.Filter<TowerBase>().Inc<HasTarget>()
            .Inc<CrystalTower>().Exc<Cooldown>().End();

        private readonly EcsPool<HasTarget> _hasTargetPool = Startup.World.GetPool<HasTarget>();

        public void Run(EcsSystems systems)
        {
            foreach (var towerEntity in _towersFilter)
            {
                ref var target = ref _hasTargetPool.Get(towerEntity).Target;
                ref var tower = ref Startup.World.GetPool<TowerBase>().Get(towerEntity);
                var projectile = PoolContainer.Instance.CrystalProjectiles.Get(tower.ShootPoint.position);
                projectile.Fire(target);

                Startup.World.GetPool<Cooldown>().Add(towerEntity).TimeLeft = tower.Stats.AttackCooldown;
            }
        }
    }
}