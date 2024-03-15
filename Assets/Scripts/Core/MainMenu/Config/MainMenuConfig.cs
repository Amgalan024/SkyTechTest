using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.MainMenu.Config
{
    [CreateAssetMenu(fileName = nameof(MainMenuConfig), menuName = "Configs/" + nameof(MainMenuConfig))]
    public class MainMenuConfig : ScriptableObject
    {
        [field: SerializeField] public Scene GameplayScene { get; private set; }
    }
}