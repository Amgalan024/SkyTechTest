using Services.LoadingScreen.SetupData;
using UnityEngine;

namespace Core.MainMenu.Config
{
    [CreateAssetMenu(fileName = nameof(MainMenuSwitchConfig), menuName = "Configs/SwitchConfigs/" + nameof(MainMenuSwitchConfig))]
    public class MainMenuSwitchConfig : ScriptableObject
    {
        [field: SerializeField] public string MainMenuScene { get; private set; }
        [field: SerializeField] public DefaultLoadingScreenSetupData LoadingScreenSetupData { get; private set; }
    }
}