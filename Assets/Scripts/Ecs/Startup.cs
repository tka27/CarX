using Ecs.Systems;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs
{
    public class Startup : MonoBehaviour
    {
        public static EcsWorld World { get; private set; }
        private EcsSystems _systems;

        void Start()
        {
            World = new();
            _systems = new EcsSystems(World);
            _systems
                .Add(new MonsterSpawnSystem())
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
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