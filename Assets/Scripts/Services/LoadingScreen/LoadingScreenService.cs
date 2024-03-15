using UnityEngine.Assertions;

namespace Utils.LoadingScreen
{
    public class LoadingScreenService
    {
        private readonly LoadingScreenProvider _loadingScreenProvider;
        private BaseLoadingScreenView _activeLoadingScreenView;

        public LoadingScreenService(LoadingScreenProvider loadingScreenProvider)
        {
            _loadingScreenProvider = loadingScreenProvider;
        }

        public void Show<T>(object setupData) where T : BaseLoadingScreenView
        {
            var loadingScreen = _loadingScreenProvider.GetLoadingScreen<T>();
            Assert.IsNotNull(loadingScreen);
            
            _activeLoadingScreenView = loadingScreen;

            loadingScreen.Setup(setupData);
            loadingScreen.Show();
        }

        public void Close<T>() where T : BaseLoadingScreenView
        {
            var loadingScreen = _loadingScreenProvider.GetLoadingScreen<T>();
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