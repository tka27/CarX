using System;
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
        [field: SerializeField] public float AimSpeed { get; private set; }
        [field: SerializeField] public Transform HorizontalAimTransform { get; private set; }
        [field: SerializeField] public Transform VerticalAimTransform { get; private set; }
    }
}