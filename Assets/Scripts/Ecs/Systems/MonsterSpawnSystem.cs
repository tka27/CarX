using Game.Scripts.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Systems
{
    public sealed class MonsterSpawnSystem : IEcsRunSystem
    {
        private float _cooldownLeft;

        public void Run(EcsSystems systems)
        {
            _cooldownLeft += Time.deltaTime;
            if (_cooldownLeft < LevelData.Instance.MonstersSpawnCooldown) return;

            _cooldownLeft -= LevelData.Instance.MonstersSpawnCooldown;
            SpawnMonster();
        }

        private void SpawnMonster()
        {
            PoolContainer.Instance.Monsters.Get(LevelData.Instance.MonstersSpawnPoint.position);
        }
    }
}