using Cysharp.Threading.Tasks;

namespace Core.PreloadLogic
{
    /// <summary>
    /// Интерфейс для точек входа у которых имеется ожидание загрузки(прелоада), на данный момент идет фейковое ожидание загрузки в виде дилеев
    /// </summary>
    public interface IEntryPointWithPreload
    {
        UniTask Prepare();
        UniTask Preload();
    }
}