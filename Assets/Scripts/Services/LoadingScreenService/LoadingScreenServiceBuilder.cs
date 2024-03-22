using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Services.LoadingScreen
{
    public class LoadingScreenServiceBuilder : BaseInstantServiceBuilder
    {
        [SerializeField] private List<BaseLoadingScreenView> _loadingScreenViews;
        private LoadingScreenService _loadingScreenService;

        public override object BuildService()
        {
            var loadingScreenProvider = new LoadingScreenProvider(_loadingScreenViews);
            _loadingScreenService = new LoadingScreenService(loadingScreenProvider);
            return _loadingScreenService;
        }

        public override void RegisterService(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadingScreenService);
        }
    }
}