using System;
using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using VContainer;

namespace Core.MainMenu.LoadingSteps
{
    public class DelayLoadingStep : ILoadingStep
    {
        public string Name { get; }

        private readonly float _delay;

        public DelayLoadingStep(string name, float delay)
        {
            Name = name;
            _delay = delay;
        }

        public async UniTask Load(IContainerBuilder builder)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_delay));
        }
    }
}