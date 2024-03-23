using System;
using System.Collections.Generic;
using AppSections.MainMenu.LoadingSteps;
using AppSections.Shared.Configs;
using Core.PreloadLogic;
using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using VContainer;

namespace AppSections.PreloadLogic
{
    /// <summary>
    /// В будущем тут могут быть различные шаги загрузки перед показом главного меню, ожидание загрузки тяжелого UI например
    /// </summary>
    public class MainMenuPreloader : IEntryPointPreloader, ILoadingInfoDispatcher
    {
        public event Action<string> OnLoadStepStarted;
        private readonly List<ISectionLoadingStep> _loadingSteps;

        /// <summary>
        /// В будущем можно отправлять сюда параметры которые регистрируются в MainMenuPreloaderRegistration, конфиги различные, префабы
        /// Для примера прокинут конфиг с имитацией загрузки
        /// </summary>
        public MainMenuPreloader(LoadDelayConfig loadDelayConfig)
        {
            _loadingSteps = new List<ISectionLoadingStep>();

            foreach (var loadDelay in loadDelayConfig.LoadDelayDataArray)
            {
                var delayLoadingStep = new DelaySectionLoadingStep(loadDelay.Name, loadDelay.Delay);
                _loadingSteps.Add(delayLoadingStep);
            }
        }

        public int GetLoadStepsCount()
        {
            return _loadingSteps.Count;
        }

        public async UniTask Preload()
        {
            foreach (var loadingStep in _loadingSteps)
            {
                OnLoadStepStarted?.Invoke(loadingStep.Name);
                await loadingStep.Load();
            }
        }

        /// <summary>
        /// В дальнейшем здесь должна быть регистрация результатов прелоадинга
        /// </summary>
        /// <param name="builder"></param>
        public void RegisterLoadedDependencies(IContainerBuilder builder)
        {
        }
    }
}