using System;

namespace Core.PreloadLogic
{
    public interface ILoadingInfoDispatcher
    {
        event Action<string> OnLoadStepStarted;

        int GetLoadStepsCount();
    }
}