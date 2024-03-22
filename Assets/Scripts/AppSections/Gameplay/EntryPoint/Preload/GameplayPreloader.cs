using System.Collections.Generic;
using AppSections.MainMenu.LoadingSteps;
using Core.PreloadLogic;
using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using VContainer;

namespace AppSections.PreloadLogic
{
    /// <summary>
    /// В будущем тут могут быть различные шаги загрузки перед началом геймплея, может быть асинхронная загрузка и инстанциирование тяжелых ассетов,
    /// может быть что то связанное с сетевым геймплеем, ожидание подключения игроков и т.д. 
    /// </summary>
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

        /// <summary>
        /// В дальнейшем здесь должна быть регистрация результатов прелоадинга
        /// </summary>
        /// <param name="builder"></param>
        public override void RegisterLoadedDependencies(IContainerBuilder builder)
        {
        }
    }
}