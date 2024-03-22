using VContainer;

namespace Services.SceneLoader
{
    public class SceneLoadServiceBuilder : BaseInstantServiceBuilder
    {
        private SceneLoadService _sceneLoadService;

        public override object BuildService()
        {
            _sceneLoadService = new SceneLoadService();
            return _sceneLoadService;
        }

        public override void RegisterService(IContainerBuilder builder)
        {
            builder.RegisterInstance(_sceneLoadService).AsImplementedInterfaces();
        }
    }
}