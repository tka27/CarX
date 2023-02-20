using Ecs.Systems;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Ecs
{
    public class Startup : MonoBehaviour
    {
        public static EcsWorld World { get; private set; }
        private EcsSystems _systems;
        private EcsSystems _fixedSystems;

        private void Awake()
        {
            World = new();
        }

        private void Start()
        {
            _systems = new EcsSystems(World);
            _fixedSystems = new EcsSystems(World);

            _systems
                .ConvertScene()
                .Add(new MonsterSpawnSystem())
                .Add(new TargetSelectionSystem())
                .Add(new CrystalTowerAttackSystem())
                .Add(new DamageSystem())
                .Add(new CooldownSystem())
                .Add(new TargetResetSystem())
                .Add(new LightningProjectilesLifeControlSystem())
                .Add(new LightningProjectilesFollowSystem())
                .Add(new CannonTowerAttackSystem())
                .Init();

            _fixedSystems
                .Add(new MonsterMoveSystem())
                .Add(new CannonTowerAimSystem())
                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedSystems?.Run();
        }


        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (World != null)
            {
                World.Destroy();
                World = null;
            }
        }
    }
}