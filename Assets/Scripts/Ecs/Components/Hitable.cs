using UnityEngine;

namespace Ecs.Components
{
    public struct Hitable
    {
        public Transform Transform;
        public int CurrentHealth;
    }
}