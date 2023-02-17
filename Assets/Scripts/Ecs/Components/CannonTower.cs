using System;
using ScriptableObjects;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Ecs.Components
{
    public sealed class CannonTower : MonoProvider<CannonTowerComponent>
    {
    }

    [Serializable]
    public struct CannonTowerComponent
    {
        public bool Aimed;
        [field: SerializeField] public float AimSpeed { get; private set; }
        [field: SerializeField] public AimingResults AimingResults { get; private set; }
        [field: SerializeField] public Transform HorizontalAimTransform { get; private set; }
        [field: SerializeField] public Transform VerticalAimTransform { get; private set; }
    }
}