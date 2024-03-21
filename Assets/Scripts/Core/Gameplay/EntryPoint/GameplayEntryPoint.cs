using System;
using System.Collections.Generic;
using Core.Gameplay.Controllers;
using Core.Gameplay.Views;
using Core.MainMenu.LoadingSteps;
using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
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

        public int LoadStepsCount => _loadingSteps.Count;

        private List<ISectionLoadingStep> _loadingSteps = new()
        {
            new DelaySectionLoadingStep("Step 1", 0.5f),
            new DelaySectionLoadingStep("Step 2", 1f),
            new DelaySectionLoadingStep("Step 3", 1f)
        };

        async UniTask IPreloadEntryPoint.PreloadEntryPoint()
        {
            foreach (var loadingStep in _loadingSteps)
            {
                OnLoadStepStarted?.Invoke(loadingStep.Name);
                await loadingStep.Load();
            }
        }

        public override void BuildEntryPoint()
        {
            Build();
        }

        protected override void Configure(IContainerBuilder builder)
        {
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