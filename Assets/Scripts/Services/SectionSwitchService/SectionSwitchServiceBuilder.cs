using SceneSwitchLogic.Switchers;
using Services;
using VContainer;

namespace SceneSwitchLogic.EntryPoint
{
    public class SectionSwitchServiceBuilder : BaseInstantServiceBuilder
    {
        private SectionSwitchService _sectionSwitchService;

        public override object BuildService()
        {
            _sectionSwitchService = new SectionSwitchService();
            return _sectionSwitchService;
        }

        public override void RegisterService(IContainerBuilder builder)
        {
            builder.RegisterInstance(_sectionSwitchService);
        }
    }
}