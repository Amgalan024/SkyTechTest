using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Utils.LoadingScreen
{
    public class LoadingScreenServiceBuilder : BaseServiceBuilder
    {
        [SerializeField] private List<BaseLoadingScreenView> _loadingScreenViews;
        private LoadingScreenService _loadingScreenService;

        public override IService Build()
        {
            var loadingScreenProvider = new LoadingScreenProvider(_loadingScreenViews);
            _loadingScreenService = new LoadingScreenService(loadingScreenProvider);
            return _loadingScreenService;
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadingScreenService);
        }
    }
}