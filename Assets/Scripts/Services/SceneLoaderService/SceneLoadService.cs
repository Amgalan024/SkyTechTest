using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Services.SceneLoader
{
    //todo: продумать абстракцию что бы подменять реализации сервисов, мб менять между ресурс и аддрессебл загрузкой
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