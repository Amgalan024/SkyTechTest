using Core.Startup;
using Core.Startup.Config;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class StartupEntryPoint : LifetimeScope
{
    [SerializeField] private StartupConfig _config;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_config);
        builder.RegisterEntryPoint<StartupController>();
    }
}