using System;
using AppSections.Gameplay.Config;
using AppSections.Gameplay.Controllers;
using AppSections.Gameplay.Views;
using AppSections.PreloadLogic;
using Core;
using Core.PreloadLogic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AppSections.Gameplay.EntryPoint
{
    /// <summary>
    /// В будущем можно будет добавлять различные типы геймплея регистрируя различные Controller'ы для геймплея
    /// например через поиск соответствующих параметров внутри SectionSwitchParams с помощью свича
    /// </summary>
    public class GameplayEntryPoint : BaseEntryPoint, IEntryPointWithPreload, ILoadingInfoDispatcher
    {
        public event Action<string> OnLoadStepStarted;

        [SerializeField] private GameplayView _gameplayView;
        [SerializeField] private GameplayConfig _gameplayConfig;
        [SerializeField] private Transform _gameplayInstantiateParent;
        [Header("Preloading")]
        [SerializeField] private GameplayPreloaderRegistration _gameplayPreloaderRegistration;
        [SerializeField] private EntryPointPreloaderRegistrar _entryPointPreloaderRegistrar;

        private GameplayPreloader _preloader;

        async UniTask IEntryPointWithPreload.Prepare()
        {
            _entryPointPreloaderRegistrar.SetEntryPointPreloaderRegistration(_gameplayPreloaderRegistration);
            await UniTask.RunOnThreadPool(() => _entryPointPreloaderRegistrar.Build());

            _preloader = _entryPointPreloaderRegistrar.GetPreloader<GameplayPreloader>();
        }

        async UniTask IEntryPointWithPreload.Preload()
        {
            _preloader.OnLoadStepStarted += stepName => { OnLoadStepStarted?.Invoke(stepName); };
            await _preloader.Preload();
        }

        int ILoadingInfoDispatcher.GetLoadStepsCount()
        {
            if (_preloader is ILoadingInfoDispatcher loadingStateDispatcher)
            {
                return loadingStateDispatcher.GetLoadStepsCount();
            }
            else
            {
                return 0;
            }
        }

        public override void BuildEntryPoint()
        {
            Build();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            _preloader.RegisterLoadedDependencies(builder);
            builder.RegisterInstance(_gameplayConfig);
            builder.RegisterInstance(SectionSwitchParams);
            builder.RegisterInstance(_gameplayView);
            builder.RegisterInstance(_gameplayInstantiateParent);

            builder.Register<FieldConstructor>(Lifetime.Singleton);
            builder.Register<GameTimer>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GameplayController>();
        }
    }
}