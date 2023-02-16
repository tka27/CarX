using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class TowerStats : ScriptableObject
    {
        [field: SerializeField] public float Range { get; private set; }

        [field: SerializeField] public float AttackCooldown { get; private set; }
    }
}