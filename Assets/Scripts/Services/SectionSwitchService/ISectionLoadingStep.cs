using Cysharp.Threading.Tasks;

namespace SceneSwitchLogic.Switchers
{
    public interface ISectionLoadingStep
    {
        string Name { get; }
        UniTask Load();
    }
}