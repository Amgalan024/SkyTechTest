using System;
using System.Collections.Generic;
using Core.MainMenu.Config;
using Core.MainMenu.Models;
using Core.MainMenu.Views;
using Core.MainMenu.Views.DialogView;
using SceneSwitchLogic.Switchers;
using UnityEngine;
using Utils.BackButtonClickDetector;
using Utils.DialogView;
using Utils.SavedDataProvider;
using VContainer.Unity;

namespace Core.MainMenu.Controller
{
    public class MainMenuController : IInitializable, IDisposable
    {
        private MainMenuModel _model;
        private MainMenuView _view;
        private MainMenuConfig _config;
        private GameplaySetupDialogView _gameplaySetupDialog;

        private readonly DialogViewService _dialogViewService;
        private readonly ISaveDataService _saveDataService;
        private readonly SectionSwitchService _sectionSwitchService;

        private readonly List<Action> _disposeActions = new();

        public MainMenuController(MainMenuView view, MainMenuModel model, MainMenuConfig config, SectionSwitchService sectionSwitchService,
            DialogViewService dialogViewService)
        {
            _view = view;
            _model = model;
            _config = config;
            _sectionSwitchService = sectionSwitchService;
            _dialogViewService = dialogViewService;
        }

        void IInitializable.Initialize()
        {
            _view.OnStartClicked += HandleStartClicked;
            _view.OnStoreClicked += HandleStoreClicked;
            BackButtonClickDetector.Instance.OnBackButtonClicked += QuitApplication;
        }

        void IDisposable.Dispose()
        {
            _view.OnStartClicked -= HandleStartClicked;
            _view.OnStoreClicked -= HandleStoreClicked;
            BackButtonClickDetector.Instance.OnBackButtonClicked -= QuitApplication;

            foreach (var disposeAction in _disposeActions)
            {
                disposeAction?.Invoke();
            }

            _disposeActions.Clear();
        }

        private void HandleStoreClicked()
        {
            _view.OpenStore();
        }

        private async void HandleStartClicked()
        {
            _gameplaySetupDialog =
                await _dialogViewService.ShowAsync<GameplaySetupDialogView>(_config.GameplaySetupData);
            _gameplaySetupDialog.OnConfirmClicked += StartGameplay;

            _disposeActions.Add(() => { _gameplaySetupDialog.OnConfirmClicked -= StartGameplay; });
        }

        private void StartGameplay()
        {
            _sectionSwitchService.Switch("Gameplay");
        }

        private void QuitApplication()
        {
            Application.Quit();
        }
    }
}