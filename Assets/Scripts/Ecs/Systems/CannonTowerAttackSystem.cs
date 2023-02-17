using Data;
using DefaultNamespace;
using Ecs.Components;
using ExtensionsMain;
using Leopotam.EcsLite;
using UnityEngine;

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
            var cannonPool = Startup.World.GetPool<CannonTowerComponent>();
            var cooldownPool = Startup.World.GetPool<CooldownComponent>();
            var towerPool = Startup.World.GetPool<TowerBaseComponent>();
            foreach (var cannonEntity in _towerFilter)
            {
                ref var cannonTower = ref cannonPool.Get(cannonEntity);
                if (!cannonTower.Aimed) continue;

                var projectile =
                    PoolContainer.Instance.CannonProjectiles.Get(cannonTower.VerticalAimTransform.position);

                projectile.Rigidbody.velocity = cannonTower.VerticalAimTransform.forward *
                                                cannonTower.AimingResults.ProjectileStartSpeed;

                cooldownPool.Add(cannonEntity).TimeLeft = towerPool.Get(cannonEntity).Stats.AttackCooldown;
            }
        }
    }
}