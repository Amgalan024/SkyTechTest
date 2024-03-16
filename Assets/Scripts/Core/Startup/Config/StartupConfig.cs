using Core.Gameplay.Config;
using SceneSwitchLogic.Switchers;
using UnityEngine;

namespace Core.Startup.Config
{
    [CreateAssetMenu(fileName = nameof(StartupConfig), menuName = "Configs/" + nameof(StartupConfig))]
    public class StartupConfig : ScriptableObject
    {
        [field: SerializeField] public MainMenuSwitchConfig MainMenuSwitchConfig { get; private set; }
        [field: SerializeField] public GameplaySwitchConfig GameplaySwitchConfig { get; private set; }
    }
}