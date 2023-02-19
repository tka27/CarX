using System;
using UnityEngine;

namespace Ecs.Components
{
    [Serializable]
    public struct Spawner
    {
        [field: SerializeField] public Transform SpawnPoint { get; private set; }
        [field: SerializeField, Min(0)] public float SpawnCooldown { get; private set; }
    }
}