using System;
using AppSections.MainMenu.Config;
using AppSections.MainMenu.Controllers;
using AppSections.MainMenu.Models;
using AppSections.MainMenu.Views;
using AppSections.PreloadLogic;
using Core;
using Core.PreloadLogic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AppSections.MainMenu.EntryPoint
{
    /// <summary>
    /// В дальнейшем главное меню можно дополнять различными подразделами через добавление новых EntryPoint в _subEntryPoints
    /// </summary>
    public class MainMenuEntryPoint : BaseEntryPoint, IPreloadEntryPoint
    {
        public event Action<string> OnLoadStepStarted;

        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private MainMenuConfig _mainMenuConfig;
        [SerializeField] private BaseEntryPoint[] _subEntryPoints;
        [SerializeField] private MainMenuPreloaderRegisterer _preloaderRegisterer;

        private MainMenuPreloader _preloader;

        async UniTask IPreloadEntryPoint.Prepare()
        {
            await UniTask.RunOnThreadPool(() => _preloaderRegisterer.Build());

            _preloader = _preloaderRegisterer.GetPreloader<MainMenuPreloader>();

            _preloader.OnLoadStepStarted += stepName => { OnLoadStepStarted?.Invoke(stepName); };

            foreach (var entryPoint in _subEntryPoints)
            {
                if (entryPoint is IPreloadEntryPoint preloadEntryPoint)
                {
                    preloadEntryPoint.OnLoadStepStarted += stepName => { OnLoadStepStarted?.Invoke(stepName); };
                    await preloadEntryPoint.Prepare();
                }
            }
        }

        int IPreloadEntryPoint.GetLoadStepsCount()
        {
            var loadSteps = 0;

            loadSteps += _preloader.GetLoadStepsCount();

            foreach (var entryPoint in _subEntryPoints)
            {
                if (entryPoint is IPreloadEntryPoint preloadEntryPoint)
                {
                    loadSteps += preloadEntryPoint.GetLoadStepsCount();
                }
            }

            return loadSteps;
        }

        async UniTask IPreloadEntryPoint.Preload()
        {
            await _preloader.Preload();

            foreach (var entryPoint in _subEntryPoints)
            {
                if (entryPoint is IPreloadEntryPoint preloadEntryPoint)
                {
                    await preloadEntryPoint.Preload();
                }
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
            _preloader.RegisterLoadedDependencies(builder);

            builder.RegisterInstance(SectionSwitchParams);

            builder.RegisterInstance(_mainMenuView);
            builder.RegisterInstance(_mainMenuConfig);

            builder.Register<MainMenuModel>(Lifetime.Singleton);
            builder.RegisterEntryPoint<MainMenuController>();
        }
    }
}