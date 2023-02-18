using System;
using ScriptableObjects;
using UnityEngine;

namespace Ecs.Components
{
    [Serializable]
    public struct CannonTower
    {
        public bool Aimed;
        [field: SerializeField] public float AimSpeed { get; private set; }
        [field: SerializeField] public AimingResults AimingResults { get; private set; }
        [field: SerializeField] public Transform HorizontalAimTransform { get; private set; }
        [field: SerializeField] public Transform VerticalAimTransform { get; private set; }
    }
}