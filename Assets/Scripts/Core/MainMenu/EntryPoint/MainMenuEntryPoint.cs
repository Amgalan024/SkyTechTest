using System;
using System.Collections.Generic;
using Core.MainMenu.Config;
using Core.MainMenu.Controllers;
using Core.MainMenu.LoadingSteps;
using Core.MainMenu.Models;
using Core.MainMenu.Views;
using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.MainMenu.EntryPoint
{
    public class MainMenuEntryPoint : BaseEntryPoint, IPreloadEntryPoint
    {
        public event Action<string> OnLoadStepStarted;

        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private MainMenuConfig _mainMenuConfig;
        [SerializeField] private BaseEntryPoint[] _subEntryPoints;

        public int LoadStepsCount => _loadingSteps.Count;

        private List<ISectionLoadingStep> _loadingSteps = new()
        {
            new DelaySectionLoadingStep("Step 1", 0.5f),
            new DelaySectionLoadingStep("Step 2", 1f),
            new DelaySectionLoadingStep("Step 3", 1f)
        };

        async UniTask IPreloadEntryPoint.PreloadEntryPoint()
        {
            foreach (var entryPoint in _subEntryPoints)
            {
                if (entryPoint is IPreloadEntryPoint preloadEntryPoint)
                {
                    await preloadEntryPoint.PreloadEntryPoint();
                }
            }

            foreach (var loadingStep in _loadingSteps)
            {
                OnLoadStepStarted?.Invoke(loadingStep.Name);
                await loadingStep.Load();
            }
        }

        public override void BuildEntryPoint()
        {
            Build();

            foreach (var entryPoint in _subEntryPoints)
            {
                entryPoint.BuildEntryPoint();
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