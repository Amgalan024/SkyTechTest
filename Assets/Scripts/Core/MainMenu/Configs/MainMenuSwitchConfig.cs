using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.LoadingScreen.SetupData;

namespace SceneSwitchLogic.Switchers
{
    public class MainMenuSwitchConfig : ScriptableObject
    {
        [field: SerializeField] public Scene MainMenuScene { get; private set; }
        [field: SerializeField] public DefaultLoadingScreenSetupData LoadingScreenSetupData { get; private set; }
    }
}