using UnityEngine;

namespace Ecs.Components
{
    public struct HitableComponent
    {
        public GameObject Provider;
        public int MaxHealth;
        public int CurrentHealth;
    }
}