using System;
using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;

namespace Core.MainMenu.LoadingSteps
{
    public class DelaySectionLoadingStep : ISectionLoadingStep
    {
        public string Name { get; }

        private readonly float _delay;

        public DelaySectionLoadingStep(string name, float delay)
        {
            Name = name;
            _delay = delay;
        }

        public async UniTask Load()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_delay));
        }
    }
}