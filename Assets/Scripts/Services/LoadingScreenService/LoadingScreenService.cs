using UnityEngine.Assertions;

namespace Services.LoadingScreen
{
    public class LoadingScreenService
    {
        private readonly LoadingScreenProvider _loadingScreenProvider;
        private BaseLoadingScreenView _activeLoadingScreenView;

        public LoadingScreenService(LoadingScreenProvider loadingScreenProvider)
        {
            _loadingScreenProvider = loadingScreenProvider;
        }

        public void Show<TLoadingScreenView>(object setupData) where TLoadingScreenView : BaseLoadingScreenView
        {
            var loadingScreen = _loadingScreenProvider.GetLoadingScreen<TLoadingScreenView>();
            Assert.IsNotNull(loadingScreen);

            _activeLoadingScreenView = loadingScreen;

            if (setupData != null)
            {
                loadingScreen.Setup(setupData);
            }

            loadingScreen.Show();
        }

        public void Close<TLoadingScreenView>() where TLoadingScreenView : BaseLoadingScreenView
        {
            var loadingScreen = _loadingScreenProvider.GetLoadingScreen<TLoadingScreenView>();
            _activeLoadingScreenView = null;

            Assert.IsNotNull(loadingScreen);

            loadingScreen.Hide();
        }

        public void SetStatus(string loadingText, float loadingProgress)
        {
            _activeLoadingScreenView.SetLoadingText(loadingText);
            _activeLoadingScreenView.SetLoadingProgress(loadingProgress);
        }
    }
}