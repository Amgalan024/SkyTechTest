using SceneSwitchLogic.Switchers;
using Utils;
using VContainer;

namespace SceneSwitchLogic.EntryPoint
{
    public class SectionSwitchServiceBuilder : BaseServiceBuilder
    {
        private SectionSwitchService _sectionSwitchService;

        public override object Build()
        {
            _sectionSwitchService = new SectionSwitchService();
            return _sectionSwitchService;
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_sectionSwitchService);
        }
    }
}