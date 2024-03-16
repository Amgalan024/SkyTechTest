using Core.Startup;
using Core.Startup.Config;
using SceneSwitchLogic.Switchers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class StartupEntryPoint : LifetimeScope
{
    [SerializeField] private StartupConfig _config;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterSwitchers(builder);

        builder.RegisterEntryPoint<StartupController>();
    }

    private void RegisterSwitchers(IContainerBuilder builder)
    {
        var mainMenuSwitchConfig = _config.MainMenuSwitchConfig;

        builder.Register<BaseSceneSwitcher>(Lifetime.Singleton)
            .WithParameter("key", "MainMenu")
            .WithParameter("scene", mainMenuSwitchConfig.MainMenuScene)
            .WithParameter("loadingScreenSetupData", mainMenuSwitchConfig.LoadingScreenSetupData);     

        var gameplaySwitchConfig = _config.GameplaySwitchConfig;

        builder.Register<BaseSceneSwitcher>(Lifetime.Singleton)
            .WithParameter("key", "Gameplay")
            .WithParameter("scene", gameplaySwitchConfig.GameplayScene)
            .WithParameter("loadingScreenSetupData", gameplaySwitchConfig.LoadingScreenSetupData);
    }
}