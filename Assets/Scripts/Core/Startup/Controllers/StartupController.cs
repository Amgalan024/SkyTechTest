using Core.Startup.Config;
using SceneSwitchLogic.Switchers;
using Services.LoadingScreen;
using Services.SceneLoader;
using VContainer.Unity;

namespace Core.Startup
{
    public class StartupController : IInitializable
    {
        private readonly StartupConfig _config;
        private readonly SectionSwitchService _sectionSwitchService;
        private readonly LoadingScreenService _loadingScreenService;
        private readonly SceneLoadService _sceneLoadService;

        public StartupController(StartupConfig config, SectionSwitchService sectionSwitchService,
            LoadingScreenService loadingScreenService, SceneLoadService sceneLoadService)
        {
            _config = config;
            _sectionSwitchService = sectionSwitchService;
            _loadingScreenService = loadingScreenService;
            _sceneLoadService = sceneLoadService;
        }

        public void Initialize()
        {
            var mainMenuSwitchConfig = _config.MainMenuSwitchConfig;
            var gameplaySwitchConfig = _config.GameplaySwitchConfig;

            var startupSwitcher = new StartupSectionSwitcher(mainMenuSwitchConfig.MainMenuScene, _loadingScreenService,
                _sceneLoadService);

            var mainMenuSwitcher = new BaseSectionSwitcher("MainMenu", mainMenuSwitchConfig.MainMenuScene,
                _loadingScreenService, _sceneLoadService, mainMenuSwitchConfig.LoadingScreenSetupData);

            var gameplaySwitcher = new BaseSectionSwitcher("Gameplay", gameplaySwitchConfig.GameplayScene,
                _loadingScreenService, _sceneLoadService, gameplaySwitchConfig.LoadingScreenSetupData);

            _sectionSwitchService.AddSwitcher(startupSwitcher);
            _sectionSwitchService.AddSwitcher(mainMenuSwitcher);
            _sectionSwitchService.AddSwitcher(gameplaySwitcher);

            _sectionSwitchService.Switch(startupSwitcher.Key);
        }
    }
}