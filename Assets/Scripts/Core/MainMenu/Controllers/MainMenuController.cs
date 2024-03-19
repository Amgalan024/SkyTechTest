using System;
using System.Collections.Generic;
using System.Linq;
using Core.Gameplay.Models;
using Core.MainMenu.Config;
using Core.MainMenu.Models;
using Core.MainMenu.Views;
using Core.MainMenu.Views.DialogView;
using SceneSwitchLogic.Switchers;
using Services.SectionSwitchService;
using UnityEngine;
using Utils.BackButtonClickDetector;
using Utils.DialogView;
using Utils.SavedDataProvider;
using VContainer.Unity;

namespace Core.MainMenu.Controller
{
    public class MainMenuController : IInitializable, IDisposable
    {
        private readonly SectionSwitchParams _sectionSwitchParams;

        private MainMenuModel _model;
        private readonly MainMenuView _view;
        private readonly MainMenuConfig _config;

        private readonly DialogViewService _dialogViewService;
        private readonly ISaveDataService _saveDataService;
        private readonly SectionSwitchService _sectionSwitchService;

        private readonly List<Action> _disposeActions = new();

        private GameplaySetupDialogView _gameplaySetupDialog;

        public MainMenuController(SectionSwitchParams sectionSwitchParams, MainMenuView view, MainMenuModel model,
            MainMenuConfig config, SectionSwitchService sectionSwitchService, DialogViewService dialogViewService)
        {
            _sectionSwitchParams = sectionSwitchParams;
            _view = view;
            _model = model;
            _config = config;
            _sectionSwitchService = sectionSwitchService;
            _dialogViewService = dialogViewService;
        }

        void IInitializable.Initialize()
        {
            var gameResult =
                _sectionSwitchParams.SwitchParams.FirstOrDefault(p => p.GetType() == typeof(GameplayResult));

            if (gameResult != null)
            {
                _dialogViewService.ShowAsync<GameplayResultDialogView>(gameResult);
            }

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
                await _dialogViewService.ShowAsync<GameplaySetupDialogView>(_config.GameplaySetupSettingsData);
            _gameplaySetupDialog.OnConfirmClicked += StartGameplay;

            _disposeActions.Add(() => { _gameplaySetupDialog.OnConfirmClicked -= StartGameplay; });
        }

        private void StartGameplay()
        {
            var gameSettings = new GameplaySettings("Player", "Bot", _gameplaySetupDialog.RoundsSliderValue,
                _gameplaySetupDialog.FieldSizeSliderValue, _gameplaySetupDialog.LineWinLenghtSliderValue);

            _sectionSwitchService.Switch("Gameplay", gameSettings);
        }

        private void QuitApplication()
        {
            Application.Quit();
        }
    }
}