using System;
using ScriptableObjects;
using UnityEngine;

namespace Ecs.Components
{
    [Serializable]
    public struct TowerBase
    {
        [field: SerializeField] public Transform ShootPoint { get; private set; }
        [field: SerializeField] public Transform SelfTransform { get; private set; }
        [field: SerializeField] public TowerStats Stats { get; private set; }
    }
}