using System.Collections.Generic;
using SceneSwitchLogic.Switchers;
using VContainer.Unity;

namespace Core.Startup
{
    public class StartupController : IInitializable
    {
        private readonly SceneSwitchService _sceneSwitchService;
        private readonly IEnumerable<ISwitcher> _switchers;

        public StartupController(IEnumerable<ISwitcher> switchers, SceneSwitchService sceneSwitchService)
        {
            _switchers = switchers;
            _sceneSwitchService = sceneSwitchService;
        }

        public void Initialize()
        {
            _sceneSwitchService.AddSwitchers(_switchers);

            _sceneSwitchService.Switch("MainMenu");
        }
    }
}