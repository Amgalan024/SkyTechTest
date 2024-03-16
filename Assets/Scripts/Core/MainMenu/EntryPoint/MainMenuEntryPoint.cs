using System;
using System.Collections.Generic;
using Core.MainMenu.Config;
using Core.MainMenu.Views;
using SceneSwitchLogic.Switchers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.MainMenu.EntryPoint
{
    public class MainMenuEntryPoint : LifetimeScope, IEntryPoint
    {
        public event Action<string> OnLoadStepStarted;

        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private MainMenuConfig _mainMenuConfig;

        private List<ILoadingStep> _loadingSteps;

        public bool LoadCompleted { get; private set; }
        public int LoadStepsCount => _loadingSteps.Count;

        void IEntryPoint.BuildEntryPoint()
        {
            Build();
        }

        protected override async void Configure(IContainerBuilder builder)
        {
            foreach (var loadingStep in _loadingSteps)
            {
                OnLoadStepStarted?.Invoke(loadingStep.Name);
                await loadingStep.Load(builder);
            }

            builder.RegisterInstance(_mainMenuView);
            builder.RegisterInstance(_mainMenuConfig);

            LoadCompleted = true;
        }
    }
}