using VContainer;
using VContainer.Unity;

namespace Core.PreloadLogic
{
    /// <summary>
    /// Наследоваться от Startup
    /// </summary>
    public class PreloaderRegisterer : LifetimeScope
    {
        public T GetPreloader<T>() where T : Preloader
        {
            return Container.Resolve<T>();
        }
    }
}