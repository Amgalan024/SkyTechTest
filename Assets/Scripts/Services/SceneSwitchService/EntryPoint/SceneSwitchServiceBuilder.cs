using SceneSwitchLogic.Switchers;
using Utils;
using VContainer;

namespace SceneSwitchLogic.EntryPoint
{
    public class SceneSwitchServiceBuilder : BaseServiceBuilder
    {
        public override void Build(IContainerBuilder builder)
        {
            builder.Register<SceneSwitchService>(Lifetime.Singleton);
        }
    }
}