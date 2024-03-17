using Core.Startup;
using Core.Startup.Config;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils.LoadingScreen;
using Utils.LoadingScreen.SetupData;
using VContainer;
using VContainer.Unity;

public class StartupEntryPoint : LifetimeScope
{
    [SerializeField] private StartupConfig _config;

    [SerializeField] private ServicesEntryPoint _servicesEntryPoint;
    [SerializeField] private DefaultLoadingScreenSetupData _loadingScreenSetupData;

    private async void Start()
    {
        DontDestroyOnLoad(this);
        _servicesEntryPoint.BuildServices();

        var loadingScreenService = _servicesEntryPoint.GetService<LoadingScreenService>();

        loadingScreenService.Show<DefaultLoadingScreen>(_loadingScreenSetupData);
        loadingScreenService.SetStatus("Services Loading", 0f);

        await UniTask.WaitUntil(() => _servicesEntryPoint.ServicesReady);

        Build();
    }

    protected override void Configure(IContainerBuilder builder)
    {
        _servicesEntryPoint.ConfigureServices(builder);

        builder.RegisterInstance(_config);
        builder.RegisterEntryPoint<StartupController>();
    }
}