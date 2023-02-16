using System;
using ScriptableObjects;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Ecs.Components
{
    public sealed class Tower : MonoProvider<TowerComponent>
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, value.Stats.Range);
        }
    }

    [Serializable]
    public struct TowerComponent
    {
        [field: SerializeField] public Transform ShootPoint { get; private set; }
        [field: SerializeField] public Transform SelfTransform { get; private set; }
        [field: SerializeField] public TowerStats Stats { get; private set; }
    }
}