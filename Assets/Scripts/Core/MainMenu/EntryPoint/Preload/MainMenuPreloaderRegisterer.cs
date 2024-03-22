using VContainer;

namespace Core.PreloadLogic
{
    public class MainMenuPreloaderRegisterer : PreloaderRegisterer
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MainMenuPreloader>(Lifetime.Singleton);
        }
    }
}