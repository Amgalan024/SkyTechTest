using System;
using UnityEngine;

namespace AppSections.Shared.Configs
{
    [Serializable]
    public class LoadDelayConfig
    {
        [field: SerializeField] public LoadDelayData[] LoadDelayDataArray { get; private set; }
    }
}