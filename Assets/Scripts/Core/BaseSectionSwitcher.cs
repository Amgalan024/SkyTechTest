using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using Services;
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
        private readonly ServicesProvider _servicesProvider;

        private readonly LoadingScreenService _loadingScreenService;
        private readonly SceneLoadService _sceneLoadService;
        private readonly DefaultLoadingScreenSetupData _loadingScreenSetupData;

        private float _progress;
        private float _stepProgress;

        public BaseSectionSwitcher(string key, string scene, ServicesProvider servicesProvider,
            DefaultLoadingScreenSetupData loadingScreenSetupData)
        {
            Key = key;
            _scene = scene;
            _servicesProvider = servicesProvider;
            _loadingScreenSetupData = loadingScreenSetupData;
            _loadingScreenService = _servicesProvider.GetService<LoadingScreenService>();
            _sceneLoadService = _servicesProvider.GetService<SceneLoadService>();
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
                await preloadEntryPoint.Prepare();

                _stepProgress = (1f - _progress) / (preloadEntryPoint.GetLoadStepsCount() + 1);
                preloadEntryPoint.OnLoadStepStarted += HandleLoadStepStarted;

                await preloadEntryPoint.Preload();

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