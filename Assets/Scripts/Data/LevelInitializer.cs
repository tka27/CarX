using SubLib.Async;
using Unity.Collections;
using UnityEngine;
using static SubLib.Utils.Editor;

namespace Data
{
    [RequireComponent(typeof(AsyncCancellation), typeof(LevelData))]
    public class LevelInitializer : MonoBehaviour, IAutoInit
    {
        [SerializeField, ReadOnly] private LevelData _levelData;
        [SerializeField, ReadOnly] private StaticData _staticData;

        public void AutoInit()
        {
            _levelData = GetComponent<LevelData>();
            _staticData = GetAllInstances<StaticData>()[0];
        }

        private void Awake()
        {
            LevelData.Instance = _levelData;
            StaticData.Instance = _staticData;
        }
    }
}