using Core.Startup.Config;
using SceneSwitchLogic.Switchers;
using Utils.LoadingScreen;
using Utils.SceneLoader;
using VContainer.Unity;

namespace Core.Startup
{
    public class StartupController : IInitializable
    {
        private readonly StartupConfig _config;
        private readonly SceneSwitchService _sceneSwitchService;
        private readonly LoadingScreenService _loadingScreenService;
        private readonly SceneLoadService _sceneLoadService;

        public StartupController(StartupConfig config, SceneSwitchService sceneSwitchService,
            LoadingScreenService loadingScreenService, SceneLoadService sceneLoadService)
        {
            _config = config;
            _sceneSwitchService = sceneSwitchService;
            _loadingScreenService = loadingScreenService;
            _sceneLoadService = sceneLoadService;
        }

        public void Initialize()
        {
            var mainMenuSwitchConfig = _config.MainMenuSwitchConfig;
            var gameplaySwitchConfig = _config.GameplaySwitchConfig;

            var startupSwitcher = new StartupSwitcher(mainMenuSwitchConfig.MainMenuScene, _loadingScreenService,
                _sceneLoadService);

            var mainMenuSwitcher = new BaseSceneSwitcher("MainMenu", mainMenuSwitchConfig.MainMenuScene,
                _loadingScreenService, _sceneLoadService, mainMenuSwitchConfig.LoadingScreenSetupData);

            var gameplaySwitcher = new BaseSceneSwitcher("Gameplay", gameplaySwitchConfig.GameplayScene,
                _loadingScreenService, _sceneLoadService, gameplaySwitchConfig.LoadingScreenSetupData);

            _sceneSwitchService.AddSwitcher(startupSwitcher);
            _sceneSwitchService.AddSwitcher(mainMenuSwitcher);
            _sceneSwitchService.AddSwitcher(gameplaySwitcher);

            _sceneSwitchService.Switch(startupSwitcher.Key);
        }
    }
}