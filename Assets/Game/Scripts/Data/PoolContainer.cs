using SubLib.ObjectPool;
using UnityEngine;

public class PoolContainer : MonoBehaviour
{
    public static PoolContainer Instance { get; private set; }
    [field: SerializeField] public ObjectPool<Transform> Monsters { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}