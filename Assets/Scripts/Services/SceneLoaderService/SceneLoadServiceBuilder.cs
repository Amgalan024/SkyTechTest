using VContainer;

namespace Utils.SceneLoader
{
    public class SceneLoadServiceBuilder : BaseServiceBuilder
    {
        private SceneLoadService _sceneLoadService;

        public override IService Build()
        {
            _sceneLoadService = new SceneLoadService();
            return _sceneLoadService;
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_sceneLoadService);
        }
    }
}