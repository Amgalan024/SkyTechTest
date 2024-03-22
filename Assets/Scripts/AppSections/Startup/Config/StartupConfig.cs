using AppSections.Gameplay.Config;
using AppSections.MainMenu.Config;
using Services.LoadingScreen.SetupData;
using UnityEngine;

namespace AppSections.Startup.Config
{
    [CreateAssetMenu(fileName = nameof(StartupConfig), menuName = "Configs/" + nameof(StartupConfig))]
    public class StartupConfig : ScriptableObject
    {
        [field: SerializeField] public MainMenuSwitchConfig MainMenuSwitchConfig { get; private set; }
        [field: SerializeField] public GameplaySwitchConfig GameplaySwitchConfig { get; private set; }
        [field: SerializeField] public DefaultLoadingScreenSetupData StartLoadingScreenSetupData { get; private set; }
    }
}