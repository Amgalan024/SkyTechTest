using Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.LoadingScreen;
using Utils.LoadingScreen.SetupData;
using Utils.SceneLoader;

namespace SceneSwitchLogic.Switchers
{
    public class BaseSceneSwitcher : ISwitcher
    {
        public string Key { get; }

        private readonly Scene _scene;

        private readonly LoadingScreenService _loadingScreenService;
        private readonly SceneLoadService _sceneLoadService;
        private readonly DefaultLoadingScreenSetupData _loadingScreenSetupData;

        private float _progress;
        private float _stepProgress;

        public BaseSceneSwitcher(string key, Scene scene, LoadingScreenService loadingScreenService,
            SceneLoadService sceneLoadService, DefaultLoadingScreenSetupData loadingScreenSetupData)
        {
            Key = key;
            _scene = scene;
            _loadingScreenService = loadingScreenService;
            _sceneLoadService = sceneLoadService;
            _loadingScreenSetupData = loadingScreenSetupData;
        }

        public async UniTask Switch()
        {
            _loadingScreenService.Show<DefaultLoadingScreen>(_loadingScreenSetupData);
            _loadingScreenService.SetStatus("Scene Loading", _progress);
            await _sceneLoadService.LoadSceneAsync(_scene);
            _progress += 0.5f;
            var entryPointHolder = Object.FindObjectOfType<EntryPointHolder>();
            var entryPoint = entryPointHolder.GetEntryPoint();

            _stepProgress = (1f - _progress) / (entryPoint.LoadStepsCount + 1);
            entryPoint.OnLoadStepStarted += HandleLoadStepStarted;

            entryPoint.BuildEntryPoint();
            await UniTask.WaitUntil(() => entryPoint.LoadCompleted);

            _progress += _stepProgress;
            _loadingScreenService.SetStatus("Completed", _progress);

            _loadingScreenService.Close<DefaultLoadingScreen>();
            entryPoint.OnLoadStepStarted -= HandleLoadStepStarted;
        }

        private void HandleLoadStepStarted(string loadingStepName)
        {
            _progress += _stepProgress;
            _loadingScreenService.SetStatus(loadingStepName, _progress);
        }
    }
}