using Utils;
using VContainer;

namespace Utils.SceneLoader
{
    public class SceneLoadServiceBuilder : BaseServiceBuilder
    {
        public override void Build(IContainerBuilder builder)
        {
            builder.Register<SceneLoadService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}