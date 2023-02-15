using UnityEngine;

namespace Ecs.Components
{
    public struct HitableComponent
    {
        public Transform Provider;
        public int MaxHealth;
        public int CurrentHealth;
    }
}