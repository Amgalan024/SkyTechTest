using Services.LoadingScreen.SetupData;
using UnityEngine;

namespace Core.Gameplay.Config
{
    [CreateAssetMenu(fileName = nameof(GameplaySwitchConfig), menuName = "Configs/SwitchConfigs/" + nameof(GameplaySwitchConfig))]
    public class GameplaySwitchConfig : ScriptableObject
    {
        [field: SerializeField] public string GameplayScene { get; private set; }
        [field: SerializeField] public DefaultLoadingScreenSetupData LoadingScreenSetupData { get; private set; }
    }
}