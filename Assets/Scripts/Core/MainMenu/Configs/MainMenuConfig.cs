﻿using Core.MainMenu.Models;
using UnityEngine;

namespace Core.MainMenu.Config
{
    [CreateAssetMenu(fileName = nameof(MainMenuConfig), menuName = "Configs/" + nameof(MainMenuConfig))]
    public class MainMenuConfig : ScriptableObject
    {
        [field: SerializeField] public GameplaySetupSettingsData GameplaySetupSettingsData { get; private set; }
    }
}