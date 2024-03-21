using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Services.SceneLoader
{
    public class SceneLoadService
    {
        public async UniTask SwitchSceneAsync(string sceneName)
        {
            var sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            await sceneLoadOperation;

            var scene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(scene);
        }
    }
}