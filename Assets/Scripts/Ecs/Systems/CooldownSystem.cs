using Ecs.Components;
using ExtensionsMain;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Systems
{
    public class CooldownSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _cooldownFilter;

        public void Init(EcsSystems systems)
        {
            _cooldownFilter = Startup.World.Filter<CooldownComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            var cooldownPool = Startup.World.GetPool<CooldownComponent>();
            foreach (var entity in _cooldownFilter)
            {
                ref var cooldown = ref cooldownPool.Get(entity);
                cooldown.TimeLeft -= Time.deltaTime;
                if (cooldown.TimeLeft < 0) cooldownPool.Del(entity);
            }
        }
    }
}