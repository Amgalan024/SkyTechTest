using Core.MainMenu.Config;
using Core.MainMenu.Model;
using Core.MainMenu.View;
using Utils.SceneLoader;
using VContainer.Unity;

namespace Core.MainMenu.Controller
{
    public class MainMenuController : IInitializable
    {
        private MainMenuModel _model;
        private MainMenuView _view;
        private MainMenuConfig _config;

        private SceneLoadService _sceneLoadService;

        public MainMenuController(MainMenuView view, MainMenuModel model, MainMenuConfig config)
        {
            _view = view;
            _model = model;
            _config = config;
        }

        void IInitializable.Initialize()
        {
        }
    }
}