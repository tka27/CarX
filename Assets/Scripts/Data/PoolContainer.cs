using MonoBehaviours;
using SubLib.ObjectPool;
using UnityEngine;

namespace Data
{
    public class PoolContainer : MonoBehaviour
    {
        public static PoolContainer Instance { get; private set; }
        [field: SerializeField] public ObjectPool<Monster> Monsters { get; private set; }
        [field: SerializeField] public ObjectPool<CannonProjectile> CannonProjectiles { get; private set; }
        [field: SerializeField] public ObjectPool<CrystalProjectile> CrystalProjectiles { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}