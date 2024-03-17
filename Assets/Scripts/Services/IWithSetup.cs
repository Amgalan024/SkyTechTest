using Cysharp.Threading.Tasks;

namespace Utils
{
    public interface IWithSetup
    {
        UniTask Setup();
    }
}