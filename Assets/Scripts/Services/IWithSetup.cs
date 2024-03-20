using Cysharp.Threading.Tasks;

namespace Services
{
    public interface IWithSetup
    {
        UniTask Setup();
    }
}