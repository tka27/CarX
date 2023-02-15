using System;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Ecs.Components
{
    public sealed class Tower : MonoProvider<TowerComponent>
    {
    }

    [Serializable]
    public struct TowerComponent
    {
        [field: SerializeField] public float Range { get; private set; }
        
        
    }
}