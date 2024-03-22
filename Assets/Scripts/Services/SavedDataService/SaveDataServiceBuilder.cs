using VContainer;

namespace Services.SavedDataProvider
{
    public class SaveDataServiceBuilder : BaseInstantServiceBuilder
    {
        private PlayerPrefsDataService _playerPrefsDataService;

        public override object BuildService()
        {
            _playerPrefsDataService = new PlayerPrefsDataService();
            return _playerPrefsDataService;
        }

        public override void RegisterService(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerPrefsDataService).AsImplementedInterfaces();
        }
    }
}