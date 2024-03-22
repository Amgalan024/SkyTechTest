using System;
using UnityEngine;

namespace AppSections.Shared.Configs
{
    [Serializable]
    public class LoadDelayData
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
    }
}