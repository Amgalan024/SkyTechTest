using VContainer;

namespace Core.PreloadLogic
{
    /// <summary>
    /// Интерфейс предназначенный для описания логики регистрации экземпляра IEntryPointPreloader и его зависимостей. 
    /// </summary>
    public interface IEntryPointPreloaderRegistration
    {
        void RegisterPreloader(IContainerBuilder builder);
    }
}