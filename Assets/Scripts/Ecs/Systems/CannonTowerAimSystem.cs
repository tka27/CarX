using Data;
using DefaultNamespace;
using Ecs.Components;
using ExtensionsMain;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Systems
{
    public class CannonTowerAimSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _towerFilter;

        public void Init(EcsSystems systems)
        {
            _towerFilter = Startup.World.Filter<CannonTowerComponent>().Inc<HasTargetComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            var cannonPool = Startup.World.GetPool<CannonTowerComponent>();
            var hasTargetPool = Startup.World.GetPool<HasTargetComponent>();
            var movablePool = Startup.World.GetPool<MovableComponent>();
            foreach (var cannonEntity in _towerFilter)
            {
                ref var cannonTower = ref cannonPool.Get(cannonEntity);
                hasTargetPool.Get(cannonEntity).Target.Unpack(Startup.World, out var targetEntity);

                var targetRigidbody = movablePool.Get(targetEntity).Rigidbody;


                Vector3 predictionPoint =
                    PredictionCalculator.GetPredictionPoint(cannonTower.HorizontalAimTransform, targetRigidbody,
                        cannonTower.AimingResults.ProjectileStartSpeed);

                float distanceToTarget = (predictionPoint - cannonTower.HorizontalAimTransform.position).magnitude;
                float verticalAngle = cannonTower.AimingResults.GetVerticalAngle(distanceToTarget);
                cannonTower.VerticalAimTransform.localRotation = Quaternion.Euler(new Vector3(verticalAngle, 0, 0));

                Debug.DrawRay(cannonTower.HorizontalAimTransform.position,
                    predictionPoint - cannonTower.HorizontalAimTransform.position);
                cannonTower.HorizontalAimTransform.HorizontalLookAt(predictionPoint);
            }
        }
    }
}