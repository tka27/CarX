using UnityEngine;

namespace Ecs.Components
{
    public struct HitableComponent
    {
        public Transform Transform;
        public int MaxHealth;
        public int CurrentHealth;
    }
}