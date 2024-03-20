using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using Services.LoadingScreen;
using Services.LoadingScreen.SetupData;
using Services.SceneLoader;
using UnityEngine;

namespace Core
{
    public class BaseSectionSwitcher : ISectionSwitcher
    {
        public string Key { get; }

        private readonly string _scene;

        private readonly LoadingScreenService _loadingScreenService;
        private readonly SceneLoadService _sceneLoadService;
        private readonly DefaultLoadingScreenSetupData _loadingScreenSetupData;

        private float _progress;
        private float _stepProgress;

        public BaseSectionSwitcher(string key, string scene, LoadingScreenService loadingScreenService,
            SceneLoadService sceneLoadService, DefaultLoadingScreenSetupData loadingScreenSetupData)
        {
            Key = key;
            _scene = scene;
            _loadingScreenService = loadingScreenService;
            _sceneLoadService = sceneLoadService;
            _loadingScreenSetupData = loadingScreenSetupData;
        }

        public async UniTask Switch(params object[] switchParams)
        {
            _loadingScreenService.Show<DefaultLoadingScreen>(_loadingScreenSetupData);
            _loadingScreenService.SetStatus("Scene Loading", _progress);
            await _sceneLoadService.SwitchSceneAsync(_scene);
            _progress += 0.5f;
            var entryPointHolder = Object.FindObjectOfType<EntryPointHolder>();
            var entryPoint = entryPointHolder.EntryPoint;
            entryPoint.SectionSwitchParams.SwitchParams.AddRange(switchParams);

            _stepProgress = (1f - _progress);

            if (entryPoint is IPreloadEntryPoint preloadEntryPoint)
            {
                _stepProgress = (1f - _progress) / (preloadEntryPoint.LoadStepsCount + 1);
                preloadEntryPoint.OnLoadStepStarted += HandleLoadStepStarted;

                await preloadEntryPoint.PreloadEntryPoint();

                preloadEntryPoint.OnLoadStepStarted -= HandleLoadStepStarted;
            }

            entryPoint.BuildEntryPoint();

            _progress += _stepProgress;
            _loadingScreenService.SetStatus("Completed", _progress);

            _loadingScreenService.Close<DefaultLoadingScreen>();
        }

        private void HandleLoadStepStarted(string loadingStepName)
        {
            _progress += _stepProgress;
            _loadingScreenService.SetStatus(loadingStepName, _progress);
        }
    }
}