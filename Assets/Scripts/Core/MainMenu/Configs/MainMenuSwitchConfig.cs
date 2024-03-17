﻿using UnityEngine;
using Utils.LoadingScreen.SetupData;

namespace SceneSwitchLogic.Switchers
{
    [CreateAssetMenu(fileName = nameof(MainMenuSwitchConfig), menuName = "Configs/SwitchConfigs/" + nameof(MainMenuSwitchConfig))]
    public class MainMenuSwitchConfig : ScriptableObject
    {
        [field: SerializeField] public string MainMenuScene { get; private set; }
        [field: SerializeField] public DefaultLoadingScreenSetupData LoadingScreenSetupData { get; private set; }
    }
}