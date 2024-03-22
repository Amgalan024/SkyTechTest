using VContainer;

namespace Core.PreloadLogic
{
    /// <summary>
    /// Интерфейс предназначенный для логики подготовки\регистрации экземпляров IEntryPointPreloader которые в свою очередь отвечают за логику прелоада и регистрацию результатов
    /// </summary>
    public interface IEntryPointPreloaderRegistration
    {
        void RegisterPreloader(IContainerBuilder builder);
    }
}