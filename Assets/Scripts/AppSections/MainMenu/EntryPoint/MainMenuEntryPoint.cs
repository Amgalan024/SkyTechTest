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
    public class MainMenuEntryPoint : BaseEntryPoint, IEntryPointWithPreload, ILoadingInfoDispatcher
    {
        public event Action<string> OnLoadStepStarted;

        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private MainMenuConfig _mainMenuConfig;
        [SerializeField] private BaseEntryPoint[] _subEntryPoints;
        [Header("Preloading")]
        [SerializeField] private MainMenuPreloaderRegistration _mainMenuPreloaderRegistration;
        [SerializeField] private EntryPointPreloaderRegistrar _entryPointPreloaderRegistrar;
        
        private MainMenuPreloader _preloader;

        async UniTask IEntryPointWithPreload.Prepare()
        {
            _entryPointPreloaderRegistrar.SetEntryPointPreloaderRegistration(_mainMenuPreloaderRegistration);
            await UniTask.RunOnThreadPool(() => _entryPointPreloaderRegistrar.Build());

            _preloader = _entryPointPreloaderRegistrar.GetPreloader<MainMenuPreloader>();

            _preloader.OnLoadStepStarted += stepName => { OnLoadStepStarted?.Invoke(stepName); };

            foreach (var entryPoint in _subEntryPoints)
            {
                if (entryPoint is ILoadingInfoDispatcher loadingStateDispatcher)
                {
                    loadingStateDispatcher.OnLoadStepStarted += stepName => { OnLoadStepStarted?.Invoke(stepName); };
                }

                if (entryPoint is IEntryPointWithPreload preloadEntryPoint)
                {
                    await preloadEntryPoint.Prepare();
                }
            }
        }

        async UniTask IEntryPointWithPreload.Preload()
        {
            await _preloader.Preload();

            foreach (var entryPoint in _subEntryPoints)
            {
                if (entryPoint is IEntryPointWithPreload preloadEntryPoint)
                {
                    await preloadEntryPoint.Preload();
                }
            }
        }

        int ILoadingInfoDispatcher.GetLoadStepsCount()
        {
            var loadSteps = 0;

            if (_preloader is ILoadingInfoDispatcher loadingStateDispatcher)
            {
                loadSteps += loadingStateDispatcher.GetLoadStepsCount();
            }

            foreach (var entryPoint in _subEntryPoints)
            {
                if (entryPoint is ILoadingInfoDispatcher subLoadingStateDispatcher)
                {
                    loadSteps += subLoadingStateDispatcher.GetLoadStepsCount();
                }
            }

            return loadSteps;
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