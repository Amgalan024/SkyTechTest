using Core.Startup;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class StartupEntryPoint : LifetimeScope
{
    [SerializeField] private Scene _startScene;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_startScene);

        builder.RegisterEntryPoint<StartupController>();
    }
}