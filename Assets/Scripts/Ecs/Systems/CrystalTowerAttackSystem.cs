using Data;
using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class CrystalTowerAttackSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _towersFilter;

        public void Init(EcsSystems systems)
        {
            _towersFilter = Startup.World.Filter<TowerBaseComponent>().Inc<HasTargetComponent>()
                .Inc<CrystalTowerComponent>().Exc<CooldownComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var towerEntity in _towersFilter)
            {
                var hasTargetPool = Startup.World.GetPool<HasTargetComponent>();

                ref var target = ref hasTargetPool.Get(towerEntity).Target;
                ref var tower = ref Startup.World.GetPool<TowerBaseComponent>().Get(towerEntity);
                var projectile = PoolContainer.Instance.CrystalProjectiles.Get(tower.ShootPoint.position);
                projectile.Fire(target);

                Startup.World.GetPool<CooldownComponent>().Add(towerEntity).TimeLeft = tower.Stats.AttackCooldown;
            }
        }
    }
}