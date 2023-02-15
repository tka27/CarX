using System;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Ecs.Components
{
    public sealed class Tower : MonoProvider<TowerComponent>
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, value.Range);
        }
    }

    [Serializable]
    public struct TowerComponent
    {
        [field: SerializeField] public Transform Provider { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
    }
}