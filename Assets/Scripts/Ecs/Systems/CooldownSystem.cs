using Ecs.Components;
using ExtensionsMain;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Systems
{
    public class CooldownSystem : IEcsRunSystem
    {
        private EcsFilter _cooldownFilter = Startup.World.Filter<Cooldown>().End();

        private readonly EcsPool<Cooldown> _cooldownPool = Startup.World.GetPool<Cooldown>();


        public void Run(EcsSystems systems)
        {
            foreach (var entity in _cooldownFilter)
            {
                ref var cooldown = ref _cooldownPool.Get(entity);
                cooldown.TimeLeft -= Time.deltaTime;
                if (cooldown.TimeLeft < 0) _cooldownPool.Del(entity);
            }
        }
    }
}