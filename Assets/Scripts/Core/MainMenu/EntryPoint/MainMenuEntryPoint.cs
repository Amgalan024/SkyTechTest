using System.Collections.Generic;
using Core.MainMenu.Config;
using Core.MainMenu.Controller;
using Core.MainMenu.LoadingSteps;
using Core.MainMenu.Models;
using Core.MainMenu.Views;
using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using Services.SectionSwitchService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.MainMenu.EntryPoint
{
    public class MainMenuEntryPoint : BaseEntryPoint
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private MainMenuConfig _mainMenuConfig;
        [SerializeField] private BaseEntryPoint[] _subEntryPoints;

        public override int LoadStepsCount => 1;

        private List<ISectionLoadingStep> _loadingSteps = new()
        {
            new DelaySectionLoadingStep("Step 1", 0.5f),
            new DelaySectionLoadingStep("Step 2", 1f),
            new DelaySectionLoadingStep("Step 3", 1f)
        };

        public override void BuildEntryPoint()
        {
            Build();

            foreach (var entryPoint in _subEntryPoints)
            {
                entryPoint.BuildEntryPoint();
            }
        }

        public override async UniTask PreloadEntryPoint()
        {
            foreach (var entryPoint in _subEntryPoints)
            {
                await entryPoint.PreloadEntryPoint();
            }

            foreach (var loadingStep in _loadingSteps)
            {
                InvokeLoadStepStart(loadingStep.Name);
                await loadingStep.Load();
            }
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(SectionSwitchParams);

            builder.RegisterInstance(_mainMenuView);
            builder.RegisterInstance(_mainMenuConfig);

            builder.Register<MainMenuModel>(Lifetime.Singleton);
            builder.RegisterEntryPoint<MainMenuController>();
        }
    }
}