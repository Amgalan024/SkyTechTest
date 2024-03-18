using Cysharp.Threading.Tasks;

namespace SceneSwitchLogic.Switchers
{
    public interface ILoadingStep
    {
        string Name { get; }
        UniTask Load();
    }
}