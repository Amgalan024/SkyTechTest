using VContainer;

namespace Core.PreloadLogic
{
    public class GameplayPreloaderRegisterer : PreloaderRegisterer
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameplayPreloader>(Lifetime.Singleton);
        }
    }
}