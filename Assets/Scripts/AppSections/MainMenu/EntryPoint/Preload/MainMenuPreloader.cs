using System.Collections.Generic;
using AppSections.MainMenu.LoadingSteps;
using Core.PreloadLogic;
using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using VContainer;

namespace AppSections.PreloadLogic
{
    /// <summary>
    /// В будущем тут могут быть различные шаги загрузки перед показом главного меню, ожидание загрузки тяжелого UI например
    /// </summary>
    public class MainMenuPreloader : Preloader
    {
        private readonly List<ISectionLoadingStep> _loadingSteps;

        public MainMenuPreloader()
        {
            _loadingSteps = new List<ISectionLoadingStep>
            {
                new DelaySectionLoadingStep("Main Menu Step 1", 0.5f),
                new DelaySectionLoadingStep("Main Menu Step 2", 1f),
                new DelaySectionLoadingStep("Main Menu Step 3", 1f)
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

        /// <summary>
        /// В дальнейшем здесь должна быть регистрация результатов прелоадинга
        /// </summary>
        /// <param name="builder"></param>
        public override void RegisterLoadedDependencies(IContainerBuilder builder)
        {
            
        }
    }
}