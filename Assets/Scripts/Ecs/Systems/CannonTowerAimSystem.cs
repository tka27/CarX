using Data;
using Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Systems
{
    public class CannonTowerAimSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _towerFilter;

        public void Init(EcsSystems systems)
        {
            _towerFilter = Startup.World.Filter<CannonTower>().Inc<HasTarget>().End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var cannonEntity in _towerFilter)
            {
                var cannonPool = Startup.World.GetPool<CannonTower>();
                var hasTargetPool = Startup.World.GetPool<HasTarget>();
                var movablePool = Startup.World.GetPool<Movable>();
                var towerPool = Startup.World.GetPool<TowerBaseComponent>();


                ref var cannonTower = ref cannonPool.Get(cannonEntity);
                ref var towerBase = ref towerPool.Get(cannonEntity);

                hasTargetPool.Get(cannonEntity).Target.Unpack(Startup.World, out var targetEntity);
                var targetRigidbody = movablePool.Get(targetEntity).Rigidbody;

                Vector3 predictionPoint = PredictionCalculator.GetPredictionPoint(towerBase.ShootPoint,
                    targetRigidbody, cannonTower.AimingResults.Projectile.Speed);

                float distanceToTarget = (predictionPoint - towerBase.SelfTransform.position).magnitude;
                float verticalAngle = cannonTower.AimingResults.GetVerticalAngle(distanceToTarget);

                cannonTower.VerticalAimTransform.localRotation = Quaternion.RotateTowards(
                    cannonTower.VerticalAimTransform.localRotation, Quaternion.Euler(verticalAngle, 0, 0),
                    cannonTower.AimSpeed * Time.fixedDeltaTime);


                var lookDirection = predictionPoint - cannonTower.HorizontalAimTransform.position;
                lookDirection.y = 0;
                var lookRotation = Quaternion.LookRotation(lookDirection);

                cannonTower.HorizontalAimTransform.rotation =
                    Quaternion.RotateTowards(cannonTower.HorizontalAimTransform.rotation, lookRotation,
                        cannonTower.AimSpeed * Time.fixedDeltaTime);


                var angle = SubLib.Utils.Vector3.ProjectionAngle(cannonTower.HorizontalAimTransform.forward,
                    predictionPoint - towerBase.SelfTransform.position, Vector3.up);
                cannonTower.Aimed = angle < float.Epsilon;

#if UNITY_EDITOR
                Debug.DrawRay(cannonTower.HorizontalAimTransform.position,
                    predictionPoint - cannonTower.HorizontalAimTransform.position, Color.black);
#endif
            }
        }
    }
}