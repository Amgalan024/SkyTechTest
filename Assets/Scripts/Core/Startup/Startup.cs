using Core.Startup.Config;
using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using Services;
using Services.LoadingScreen;
using Services.SceneLoader;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Startup
{
    public class Startup : LifetimeScope
    {
        [SerializeField] private StartupConfig _config;
        [SerializeField] private ServicesProvider _servicesProvider;

        private SectionSwitchService _sectionSwitchService;
        private LoadingScreenService _loadingScreenService;
        private SceneLoadService _sceneLoadService;
        private float _progress;
        private float _stepProgress;

        private async void Start()
        {
            DontDestroyOnLoad(this);
            _servicesProvider.BuildInstantServices();

            _loadingScreenService = _servicesProvider.GetService<LoadingScreenService>();
            _sectionSwitchService = _servicesProvider.GetService<SectionSwitchService>();
            _sceneLoadService = _servicesProvider.GetService<SceneLoadService>();

            _progress = 0f;
            _loadingScreenService.Show<DefaultLoadingScreen>(_config.StartLoadingScreenSetupData);
            _loadingScreenService.SetStatus("Services Loading", _progress);

            await _servicesProvider.BuildServicesWithSetup();
            _progress += 0.25f;

            CreateSwitchers();

            Build();

            SwitchToMainMenu();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            _servicesProvider.ConfigureServices(builder);
        }

        private void CreateSwitchers()
        {
            var mainMenuSwitchConfig = _config.MainMenuSwitchConfig;
            var gameplaySwitchConfig = _config.GameplaySwitchConfig;

            var mainMenuSwitcher = new BaseSectionSwitcher("MainMenu", mainMenuSwitchConfig.MainMenuScene,
                _servicesProvider, mainMenuSwitchConfig.LoadingScreenSetupData);

            var gameplaySwitcher = new BaseSectionSwitcher("Gameplay", gameplaySwitchConfig.GameplayScene,
                _servicesProvider, gameplaySwitchConfig.LoadingScreenSetupData);

            _sectionSwitchService.AddSwitcher(mainMenuSwitcher);
            _sectionSwitchService.AddSwitcher(gameplaySwitcher);
        }

        private async UniTask SwitchToMainMenu()
        {
            _loadingScreenService.SetStatus("Loading Menu Scene", _progress);

            await _sceneLoadService.SwitchSceneAsync(_config.MainMenuSwitchConfig.MainMenuScene);
            _progress += 0.25f;

            var entryPointHolder = FindObjectOfType<EntryPointHolder>();
            var entryPoint = entryPointHolder.EntryPoint;
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