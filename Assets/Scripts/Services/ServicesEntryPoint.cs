using UnityEngine;
using Utils;
using VContainer;
using VContainer.Unity;

public class ServicesEntryPoint : LifetimeScope
{
    [SerializeField] private BaseServiceBuilder[] _baseServiceBuilders;

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this);
    }

    protected override void Configure(IContainerBuilder builder)
    {
        foreach (var serviceBuilder in _baseServiceBuilders)
        {
            serviceBuilder.Build(builder);
        }
    }
}