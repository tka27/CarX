using System;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Ecs.Components
{
    public sealed class Movable : MonoProvider<MovableComponent>
    {
    }

    [Serializable]
    public struct MovableComponent
    {
        [field: SerializeField, Min(0)] public float Speed { get; private set; }
    }
}