using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.LoadingScreen.SetupData;

namespace Core.Gameplay.Config
{
    [CreateAssetMenu(fileName = nameof(GameplaySwitchConfig), menuName = "Configs/" + nameof(GameplaySwitchConfig))]
    public class GameplaySwitchConfig : ScriptableObject
    {
        [field: SerializeField] public Scene GameplayScene { get; private set; }
        [field: SerializeField] public DefaultLoadingScreenSetupData LoadingScreenSetupData { get; private set; }
    }
}