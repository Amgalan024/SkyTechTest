using System;

namespace Core
{
    public interface IEntryPoint
    {
        event Action<string> OnLoadStepStarted;
        int LoadStepsCount { get; }
        bool LoadCompleted { get; }
        void BuildEntryPoint();
    }
}