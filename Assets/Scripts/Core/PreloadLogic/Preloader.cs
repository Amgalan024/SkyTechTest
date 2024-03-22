using System;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Core
{
    public abstract class Preloader
    {
        public event Action<string> OnLoadStepStarted;

        public abstract int GetLoadStepsCount();

        public abstract UniTask Preload();
        public abstract void RegisterLoadedDependencies(IContainerBuilder builder);

        protected void InvokeLoadStepStart(string loadStepName)
        {
            OnLoadStepStarted?.Invoke(loadStepName);
        }
    }
}