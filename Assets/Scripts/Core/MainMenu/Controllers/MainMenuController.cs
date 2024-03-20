using System;
using System.Collections.Generic;
using System.Linq;
using Core.Gameplay.InputStrategies;
using Core.Gameplay.Models;
using Core.MainMenu.Config;
using Core.MainMenu.Models;
using Core.MainMenu.Views;
using Core.MainMenu.Views.DialogView;
using SceneSwitchLogic.Switchers;
using Services.DialogView;
using Services.DialogView.SetupData;
using Services.DialogView.Views;
using Services.SavedDataProvider;
using Services.SectionSwitchService;
using UnityEngine;
using Utils.BackButtonClickDetector;
using VContainer.Unity;

namespace Core.MainMenu.Controllers
{
    public class MainMenuController : IInitializable, IDisposable
    {
        private readonly SectionSwitchParams _sectionSwitchParams;

        private readonly MainMenuModel _model;
        private readonly MainMenuView _view;
        private readonly MainMenuConfig _config;

        private readonly DialogViewService _dialogViewService;
        private readonly ISaveDataService _saveDataService;
        private readonly SectionSwitchService _sectionSwitchService;

        private readonly List<Action> _disposeActions = new();

        private GameplaySetupDialogView _gameplaySetupDialog;

        public MainMenuController(SectionSwitchParams sectionSwitchParams, MainMenuView view, MainMenuModel model,
            MainMenuConfig config, SectionSwitchService sectionSwitchService, DialogViewService dialogViewService,
            ISaveDataService saveDataService)
        {
            _sectionSwitchParams = sectionSwitchParams;
            _view = view;
            _model = model;
            _config = config;
            _sectionSwitchService = sectionSwitchService;
            _dialogViewService = dialogViewService;
            _saveDataService = saveDataService;
        }

        void IInitializable.Initialize()
        {
            var gameResult =
                _sectionSwitchParams.SwitchParams.FirstOrDefault(p => p.GetType() == typeof(GameplayResult));

            if (gameResult != null)
            {
                _dialogViewService.ShowAsync<GameplayResultDialogView>(gameResult);
            }

            var score = _saveDataService.GetData<int>("Score");
            _view.SetScoreText(score);

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
            var playerInputStrategyModel = new PlayerInputStrategyModel("Player", "1");
            var botStrategyInputModel = new BotStrategyInputModel("Bot", 1, "2");

            var gameSettings = new GameplaySettings(playerInputStrategyModel, botStrategyInputModel,
                _gameplaySetupDialog.RoundsSliderValue, _gameplaySetupDialog.FieldSizeSliderValue,
                _gameplaySetupDialog.LineWinLenghtSliderValue, _config.GameplaySetupSettingsData.ScoreRewards);

            _sectionSwitchService.Switch("Gameplay", gameSettings);
        }

        private async void QuitApplication()
        {
            var confirmationSetupData = new ConfirmationSetupData()
            {
                HeaderText = "Exit",
                DescriptionText = "Are you sure you want to exit"
            };

            var dialog = await _dialogViewService.ShowAsync<ConfirmationDialogView>(confirmationSetupData);
            dialog.OnConfirmClicked += OnExitConfirmClicked;
        }

        private void OnExitConfirmClicked(bool confirmed)
        {
            if (confirmed)
            {
                Application.Quit();
            }
        }
    }
}