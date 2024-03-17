using SceneSwitchLogic.Switchers;
using Utils;
using VContainer;

namespace SceneSwitchLogic.EntryPoint
{
    public class SceneSwitchServiceBuilder : BaseServiceBuilder
    {
        private SceneSwitchService _sceneSwitchService;

        public override IService Build()
        {
            _sceneSwitchService = new SceneSwitchService();
            return _sceneSwitchService;
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_sceneSwitchService);
        }
    }
}