using Data;
using Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Systems
{
    public sealed class MonsterSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter = Startup.World.Filter<Spawner>().Exc<Cooldown>().End();
        private readonly EcsPool<Spawner> _spawnerPool = Startup.World.GetPool<Spawner>();
        private readonly EcsPool<Cooldown> _cooldownPool = Startup.World.GetPool<Cooldown>();

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var spawner = ref _spawnerPool.Get(entity);

                PoolContainer.Instance.Monsters.Get(spawner.SpawnPoint.position);
                _cooldownPool.Add(entity).TimeLeft = spawner.SpawnCooldown;
            }
        }
    }
}