using System;
using Cysharp.Threading.Tasks;
using Services.SectionSwitchService;
using VContainer.Unity;

namespace Core
{
    public abstract class BaseEntryPoint : LifetimeScope
    {

        public SectionSwitchParams SectionSwitchParams { get; } = new();

        public abstract void BuildEntryPoint();
    }
}