using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using Services.LoadingScreen;
using Services.SceneLoader;
using UnityEngine;

namespace Core.Startup
{
    public class StartupSectionSwitcher : ISectionSwitcher
    {
        public string Key => "Startup";

        private readonly string _scene;
        private readonly LoadingScreenService _loadingScreenService;
        private readonly SceneLoadService _sceneLoadService;

        private float _progress;
        private float _stepProgress;

        public StartupSectionSwitcher(string scene, LoadingScreenService loadingScreenService,
            SceneLoadService sceneLoadService)
        {
            _scene = scene;
            _loadingScreenService = loadingScreenService;
            _sceneLoadService = sceneLoadService;
        }

        public async UniTask Switch(params object[] switchParams)
        {
            //todo: добавить в сервис лоадинг скрина поле прогресс котоырй можно будет брать и от него отталкиваться при работа с одним экраном в разных частях прилы
            _loadingScreenService.SetStatus("Loading Menu Scene", _progress);

            await _sceneLoadService.SwitchSceneAsync(_scene);
            _progress += 0.25f;

            var entryPointHolder = Object.FindObjectOfType<EntryPointHolder>();
            var entryPoint = entryPointHolder.EntryPoint;
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