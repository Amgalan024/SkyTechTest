using System;

namespace Core.PreloadLogic
{
    public interface ILoadingStateDispatcher
    {
        event Action<string> OnLoadStepStarted;

        int GetLoadStepsCount();
    }
}