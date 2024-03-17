using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Utils.LoadingScreen
{
    public class LoadingScreenServiceBuilder : BaseServiceBuilder
    {
        [SerializeField] private List<BaseLoadingScreenView> _loadingScreenViews;

        public override void Build(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadingScreenViews);
            builder.Register<LoadingScreenProvider>(Lifetime.Singleton).AsSelf();
            builder.Register<LoadingScreenService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}