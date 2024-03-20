using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Services.LoadingScreen
{
    public class LoadingScreenProvider
    {
        private readonly List<BaseLoadingScreenView> _loadingScreenViews;

        public LoadingScreenProvider(List<BaseLoadingScreenView> loadingScreenViews)
        {
            _loadingScreenViews = loadingScreenViews;
        }

        public TLoadingScreen GetLoadingScreen<TLoadingScreen>() where TLoadingScreen : BaseLoadingScreenView
        {
            var loadingScreen = _loadingScreenViews.FirstOrDefault(l => l.GetType() == typeof(TLoadingScreen));

            Assert.IsNotNull(loadingScreen);

            return loadingScreen as TLoadingScreen;
        }
    }
}