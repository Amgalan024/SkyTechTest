using Cysharp.Threading.Tasks;

namespace SceneSwitchLogic.Switchers
{
    public interface ISwitcher
    {
        string Key { get; }
        UniTask Switch();
    }
}