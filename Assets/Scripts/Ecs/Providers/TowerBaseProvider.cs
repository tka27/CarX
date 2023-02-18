using Ecs.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Ecs.Providers
{
    public sealed class TowerBaseProvider : MonoProvider<TowerBase>
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, value.Stats.Range);
        }
    }
}