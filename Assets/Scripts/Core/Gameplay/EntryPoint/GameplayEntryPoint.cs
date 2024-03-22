using System;
using Core.Gameplay.Controllers;
using Core.Gameplay.Views;
using Core.PreloadLogic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Gameplay.EntryPoint
{
    /// <summary>
    /// В будущем можно будет добавлять различные типы геймплея регистрируя различные Controller'ы для геймплея
    /// например через поиск соответствующих параметров внутри SectionSwitchParams 
    /// </summary>
    public class GameplayEntryPoint : BaseEntryPoint, IPreloadEntryPoint
    {
        public event Action<string> OnLoadStepStarted;

        [SerializeField] private GameplayView _gameplayView;
        [SerializeField] private FieldView _fieldView;
        [SerializeField] private FieldCellView _fieldCellPrefab;
        [SerializeField] private GameplayPreloaderRegisterer _preloaderRegisterer;

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
            builder.RegisterInstance(SectionSwitchParams);
            builder.RegisterInstance(_gameplayView);
            builder.RegisterInstance(_fieldView);
            builder.RegisterInstance(_fieldCellPrefab);

            builder.Register<FieldConstructor>(Lifetime.Singleton);
            builder.Register<GameTimer>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GameplayController>();
        }
    }
}