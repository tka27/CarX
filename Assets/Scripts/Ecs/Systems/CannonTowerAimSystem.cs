using Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Systems
{
    public class CannonTowerAimSystem : IEcsRunSystem
    {
        private readonly EcsFilter _towerFilter = Startup.World.Filter<CannonTower>().Inc<HasTarget>().End();
        private readonly EcsPool<CannonTower> _cannonPool = Startup.World.GetPool<CannonTower>();
        private readonly EcsPool<HasTarget> _hasTargetPool = Startup.World.GetPool<HasTarget>();
        private readonly EcsPool<Movable> _movablePool = Startup.World.GetPool<Movable>();
        private readonly EcsPool<TowerBase> _towerPool = Startup.World.GetPool<TowerBase>();

        public void Run(EcsSystems systems)
        {
            foreach (var cannonEntity in _towerFilter)
            {
                ref var cannonTower = ref _cannonPool.Get(cannonEntity);
                ref var towerBase = ref _towerPool.Get(cannonEntity);

                _hasTargetPool.Get(cannonEntity).Target.Unpack(Startup.World, out var targetEntity);
                var targetRigidbody = _movablePool.Get(targetEntity).Rigidbody;

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