using UnityEngine;

namespace Data
{
    public class LevelData : MonoBehaviour
    {
        public static LevelData Instance { get; private set; }


        [field: SerializeField] public Transform MonstersTarget { get; private set; }
        [field: SerializeField] public Transform MonstersSpawnPoint { get; private set; }
        [field: SerializeField, Min(0)] public float MonstersSpawnCooldown { get; private set; }
        [field: SerializeField] public float LightningProjectilesSpeed { get; private set; }
        [field: SerializeField] public float CannonProjectilesSpeed { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}