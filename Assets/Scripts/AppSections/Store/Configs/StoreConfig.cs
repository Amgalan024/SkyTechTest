using System.Collections.Generic;
using AppSections.Store.Views;
using UnityEngine;

namespace AppSections.Store.Configs
{
    [CreateAssetMenu(fileName = nameof(StoreConfig), menuName = "Configs/" + nameof(StoreConfig))]
    public class StoreConfig : ScriptableObject
    {
        [field: SerializeField] public List<BaseProductView> BaseItemViewPrefabs { get; private set; }
        [field: SerializeField] public TextAsset Products { get; private set; }
    }
}