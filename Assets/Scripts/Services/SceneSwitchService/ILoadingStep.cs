using Cysharp.Threading.Tasks;
using VContainer;

namespace SceneSwitchLogic.Switchers
{
    public interface ILoadingStep
    {
        string Name { get; }
        UniTask Load(IContainerBuilder builder);
    }
}