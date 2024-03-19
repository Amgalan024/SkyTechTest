using Cysharp.Threading.Tasks;

namespace SceneSwitchLogic.Switchers
{
    public interface ISectionSwitcher
    {
        string Key { get; }
        UniTask Switch(params object[] switchParams);
    }
}