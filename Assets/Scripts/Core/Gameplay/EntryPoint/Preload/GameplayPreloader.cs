using System.Collections.Generic;
using Core.MainMenu.LoadingSteps;
using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using VContainer;

namespace Core.PreloadLogic
{
    public class GameplayPreloader : Preloader
    {
        private readonly List<ISectionLoadingStep> _loadingSteps;

        public GameplayPreloader()
        {
            _loadingSteps = new List<ISectionLoadingStep>
            {
                new DelaySectionLoadingStep("Gameplay Step 1", 0.5f),
                new DelaySectionLoadingStep("Gameplay Step 2", 1f),
                new DelaySectionLoadingStep("Gameplay Step 3", 1f)
            };
        }

        public override int GetLoadStepsCount()
        {
            return _loadingSteps.Count;
        }

        public override async UniTask Preload()
        {
            foreach (var loadingStep in _loadingSteps)
            {
                InvokeLoadStepStart(loadingStep.Name);
                await loadingStep.Load();
            }
        }

        public override void RegisterLoadedDependencies(IContainerBuilder builder)
        {
        }
    }
}