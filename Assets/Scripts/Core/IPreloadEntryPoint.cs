using System;
using Cysharp.Threading.Tasks;

namespace Core
{
    public interface IPreloadEntryPoint
    {
        event Action<string> OnLoadStepStarted;
        int LoadStepsCount { get; }
        UniTask PreloadEntryPoint();
    }
}