using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs
{
    public class Ecs : MonoBehaviour
    {
        public static EcsWorld World { get; private set; }
        private EcsSystems _systems;

        void Start()
        {
            World = new();
            _systems = new EcsSystems(World);
            _systems
                    
                //.Add(new TestSystem1())
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }
    }
}