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
                .Add(new CooldownSystem())
                .Init();

            _fixedSystems
                .ConvertScene()
                .Add(new MonsterMoveSystem())
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