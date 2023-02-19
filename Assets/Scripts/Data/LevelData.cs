using UnityEngine;

namespace Data
{
    public class LevelData : MonoBehaviour
    {
        public static LevelData Instance { get; private set; }

        [field: SerializeField] public Transform MonstersTarget { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}