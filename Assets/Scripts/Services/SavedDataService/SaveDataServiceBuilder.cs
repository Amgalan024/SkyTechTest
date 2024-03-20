using VContainer;

namespace Utils.SavedDataProvider
{
    public class SaveDataServiceBuilder : BaseServiceBuilder
    {
        private PlayerPrefsDataService _playerPrefsDataService;

        public override object Build()
        {
            _playerPrefsDataService = new PlayerPrefsDataService();
            return _playerPrefsDataService;
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerPrefsDataService).AsImplementedInterfaces();
        }
    }
}