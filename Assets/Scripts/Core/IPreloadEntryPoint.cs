using System;
using Cysharp.Threading.Tasks;

namespace Core
{
    public interface IPreloadEntryPoint
    {
        event Action<string> OnLoadStepStarted;
        UniTask Prepare();
        int GetLoadStepsCount();
        UniTask Preload();
    }
}