using System.Collections.Generic;
using Core.Store.View;
using UnityEngine;

namespace Core.Store.Configs
{
    [CreateAssetMenu(fileName = nameof(StoreConfig), menuName = "Configs/" + nameof(StoreConfig))]
    public class StoreConfig : ScriptableObject
    {
        [field: SerializeField] public List<BaseProductView> BaseItemViewPrefabs { get; private set; }
    }
}