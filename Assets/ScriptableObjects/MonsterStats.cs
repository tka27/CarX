using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class MonsterStats : ScriptableObject
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }
}