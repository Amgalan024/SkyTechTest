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
    public class GameplayEntryPoint : BaseEntryPoint
    {
        [SerializeField] private GameplayView _gameplayView;
        [SerializeField] private FieldView _fieldView;
        [SerializeField] private FieldCellView _fieldCellPrefab;
        public override int LoadStepsCount { get; }

        private List<ISectionLoadingStep> _loadingSteps = new()
        {
            new DelaySectionLoadingStep("Step 1", 0.5f),
            new DelaySectionLoadingStep("Step 2", 1f),
            new DelaySectionLoadingStep("Step 3", 1f)
        };

        public override void BuildEntryPoint()
        {
            Build();
        }

        public override async UniTask PreloadEntryPoint()
        {
            foreach (var loadingStep in _loadingSteps)
            {
                InvokeLoadStepStart(loadingStep.Name);
                await loadingStep.Load();
            }
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(SectionSwitchParams);
            builder.RegisterInstance(_gameplayView);
            builder.RegisterInstance(_fieldView);
            builder.RegisterInstance(_fieldCellPrefab);

            builder.Register<FieldConstructor>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GameplayController>();
        }
    }
}