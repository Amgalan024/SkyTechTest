using System;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Core
{
    public abstract class BaseEntryPoint : LifetimeScope
    {
        public event Action<string> OnLoadStepStarted;

        //todo: выделить метод в отдельный интерфейс IPreload
        public abstract int LoadStepsCount { get; }

        public abstract void BuildEntryPoint();

        //todo: выделить метод в отдельный интерфейс IPreload
        public abstract UniTask PreloadEntryPoint();

        protected void InvokeLoadStepStart(string loadStepName)
        {
            OnLoadStepStarted?.Invoke(loadStepName);
        }
    }
}