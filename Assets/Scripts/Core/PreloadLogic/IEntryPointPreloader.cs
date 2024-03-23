using Cysharp.Threading.Tasks;
using VContainer;

namespace Core.PreloadLogic
{
    /// <summary>
    /// Интерфейс предназначенный для логики ожидания загрузки(прелоада) и дальнейшей регистрации результатов згрузки(прелоада)
    /// </summary>
    public interface IEntryPointPreloader
    {
        UniTask Preload();
        void RegisterLoadedDependencies(IContainerBuilder builder);
    }
}