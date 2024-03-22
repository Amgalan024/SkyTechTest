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
    /// например через поиск соответствующих параметров внутри SectionSwitchParams 
    /// </summary>
    public class GameplayEntryPoint : BaseEntryPoint, IPreloadEntryPoint
    {
        public event Action<string> OnLoadStepStarted;

        [SerializeField] private GameplayView _gameplayView;
        [SerializeField] private GameplayConfig _gameplayConfig;
        [SerializeField] private GameplayPreloaderRegisterer _preloaderRegisterer;
        [SerializeField] private Transform _gameplayInstantiateParent;
        private GameplayPreloader _preloader;

        async UniTask IPreloadEntryPoint.Prepare()
        {
            await UniTask.RunOnThreadPool(() => _preloaderRegisterer.Build());

            _preloader = _preloaderRegisterer.GetPreloader<GameplayPreloader>();
        }

        int IPreloadEntryPoint.GetLoadStepsCount()
        {
            return _preloader.GetLoadStepsCount();
        }

        async UniTask IPreloadEntryPoint.Preload()
        {
            _preloader.OnLoadStepStarted += stepName => { OnLoadStepStarted?.Invoke(stepName); };
            await _preloader.Preload();
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