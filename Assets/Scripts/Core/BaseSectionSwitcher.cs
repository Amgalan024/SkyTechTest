using System;
using Core.PreloadLogic;
using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using Services;
using Services.LoadingScreen;
using Services.LoadingScreen.SetupData;
using Services.SceneLoader;
using Object = UnityEngine.Object;

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
            _loadingScreenSetupData = loadingScreenSetupData;
            _loadingScreenService = loadingScreenService;
            _sceneLoadService = sceneLoadService;
        }

        public async UniTask Switch(params object[] switchParams)
        {
            _progress = 0;
            _loadingScreenService.Show<DefaultLoadingScreen>(_loadingScreenSetupData);
            _loadingScreenService.SetStatus("Scene Loading", _progress);
            await _sceneLoadService.SwitchSceneAsync(_scene);
            _progress += 0.5f;
            var entryPointHolder = Object.FindObjectOfType<EntryPointHolder>();
            var entryPoint = entryPointHolder.EntryPoint;
            entryPoint.SectionSwitchParams.SwitchParams.AddRange(switchParams);
            _stepProgress = (1f - _progress);

            if (entryPoint is IEntryPointWithPreload preloadEntryPoint)
            {
                await preloadEntryPoint.Prepare();

                if (entryPoint is ILoadingInfoDispatcher loadingStateDispatcher)
                {
                    _stepProgress = (1f - _progress) / (loadingStateDispatcher.GetLoadStepsCount() + 1);
                    loadingStateDispatcher.OnLoadStepStarted += HandleLoadStepStarted;
                    await preloadEntryPoint.Preload();
                    loadingStateDispatcher.OnLoadStepStarted -= HandleLoadStepStarted;
                }
                else
                {
                    await preloadEntryPoint.Preload();
                }
            }

            entryPoint.BuildEntryPoint();

            _progress += _stepProgress;
            _loadingScreenService.SetStatus("Completed", _progress);

            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

            _loadingScreenService.Close<DefaultLoadingScreen>();
        }

        private void HandleLoadStepStarted(string loadingStepName)
        {
            _progress += _stepProgress;
            _loadingScreenService.SetStatus(loadingStepName, _progress);
        }
    }
}