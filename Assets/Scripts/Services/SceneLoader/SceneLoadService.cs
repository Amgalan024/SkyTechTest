using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Utils.SceneLoader
{
    //todo: продумать абстракции что бы подменять реализации сервисов, мб менять между ресурс и аддрессебл загрузкой
    public class SceneLoadService
    {
        public async UniTask LoadSceneAsync(Scene scene)
        {
            await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            var sceneLoadOperation = SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);

            await sceneLoadOperation;

            SceneManager.SetActiveScene(scene);
        }
    }
}