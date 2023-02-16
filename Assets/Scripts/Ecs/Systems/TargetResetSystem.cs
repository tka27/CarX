using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class TargetResetSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;

        public void Init(EcsSystems systems)
        {
            _filter = Startup.World.Filter<HasTargetComponent>().Inc<TowerComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            var hasTargetPool = Startup.World.GetPool<HasTargetComponent>();
            foreach (var entity in _filter)
            {
                if (!hasTargetPool.Get(entity).Target.Unpack(Startup.World, out var targetEntity))
                    hasTargetPool.Del(entity);
            }
        }
    }
}