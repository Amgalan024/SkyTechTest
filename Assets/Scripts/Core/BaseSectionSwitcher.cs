using Core;
using Cysharp.Threading.Tasks;
using Services.SectionSwitchService;
using UnityEngine;
using Utils.LoadingScreen;
using Utils.LoadingScreen.SetupData;
using Utils.SceneLoader;

namespace SceneSwitchLogic.Switchers
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

            _stepProgress = (1f - _progress) / (entryPoint.LoadStepsCount + 1);
            entryPoint.OnLoadStepStarted += HandleLoadStepStarted;

            entryPoint.SectionSwitchParams.SwitchParams.AddRange(switchParams);

            await entryPoint.PreloadEntryPoint();
            entryPoint.BuildEntryPoint();

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