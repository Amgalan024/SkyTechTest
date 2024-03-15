using Core.MainMenu.Config;
using Core.MainMenu.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.MainMenu.EntryPoint
{
    public class MainMenuEntryPoint : LifetimeScope
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private MainMenuConfig _mainMenuConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_mainMenuView);
            builder.RegisterInstance(_mainMenuConfig);
        }
    }
}