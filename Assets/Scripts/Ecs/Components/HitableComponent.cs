using UnityEngine;

namespace Ecs.Components
{
    public struct HitableComponent
    {
        public Transform Transform;
        public int CurrentHealth;
    }
}