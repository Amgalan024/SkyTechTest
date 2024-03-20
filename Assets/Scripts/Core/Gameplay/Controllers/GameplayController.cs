using System;
using System.Collections.Generic;
using System.Linq;
using Core.Gameplay.InputStrategies;
using Core.Gameplay.Models;
using Core.Gameplay.Views;
using SceneSwitchLogic.Switchers;
using Services.DialogView;
using Services.DialogView.SetupData;
using Services.DialogView.Views;
using Services.SavedDataProvider;
using Services.SectionSwitchService;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Core.Gameplay.Controllers
{
    public class GameplayController : IInitializable, IDisposable
    {
        private readonly SectionSwitchParams _sectionSwitchParams;
        private readonly SectionSwitchService _sectionSwitchService;

        private readonly FieldConstructor _fieldConstructor;
        private readonly DialogViewService _dialogViewService;
        private readonly ISaveDataService _saveDataService;
        private readonly GameTimer _gameTimer;
        private readonly GameplayView _view;

        private GameplaySettings _gameplaySettings;

        private readonly LinkedList<IInputStrategy> _inputStrategies = new();
        private readonly Dictionary<IInputStrategy, int> _roundsByInputStrategies = new();
        private LinkedListNode<IInputStrategy> _currentTurnInputStrategy;
        private IInputStrategy _playerInputStrategy;

        private readonly List<Vector2> _lineWinDirections = new()
        {
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(-1, 1),
        };

        private ConfirmationDialogView _exitConfirmationDialog;

        private readonly List<Action> _disposeActions = new();

        public GameplayController(SectionSwitchParams sectionSwitchParams, GameplayView view,
            SectionSwitchService sectionSwitchService, FieldConstructor fieldConstructor,
            DialogViewService dialogViewService, GameTimer gameTimer, ISaveDataService saveDataService)
        {
            _sectionSwitchParams = sectionSwitchParams;
            _view = view;
            _sectionSwitchService = sectionSwitchService;
            _fieldConstructor = fieldConstructor;
            _dialogViewService = dialogViewService;
            _gameTimer = gameTimer;
            _saveDataService = saveDataService;
        }

        void IInitializable.Initialize()
        {
            _gameplaySettings =
                (GameplaySettings) _sectionSwitchParams.SwitchParams.FirstOrDefault(p =>
                    p.GetType() == typeof(GameplaySettings));

            Assert.IsNotNull(_gameplaySettings);

            _view.OnPauseClicked += OnPauseClicked;
            _view.SetPlayerName(_gameplaySettings.PlayerInputStrategyModel.Name);
            _view.SetOpponentName(_gameplaySettings.BotStrategyInputModel.Name);
            _view.SetRoundsCounterStatus(0, _gameplaySettings.TotalRounds);

            _view.PauseView.OnResumeClicked += OnResumeClicked;
            _view.PauseView.OnMainMenuClicked += OnMainMenuClicked;

            CreateGame();
            StartRound();
        }

        void IDisposable.Dispose()
        {
            _gameTimer.Stop();

            _view.OnPauseClicked -= OnPauseClicked;
            _currentTurnInputStrategy.Value.OnInput -= SetTextTurn;
            _gameTimer.OnTick -= _view.SetTime;
            _view.PauseView.OnResumeClicked -= OnResumeClicked;
            _view.PauseView.OnMainMenuClicked -= OnMainMenuClicked;

            foreach (var disposeAction in _disposeActions)
            {
                disposeAction.Invoke();
            }
        }

        private void OnPauseClicked()
        {
            _gameTimer.Pause();
            _view.PauseView.Show();
        }

        private void OnResumeClicked()
        {
            _gameTimer.Resume();
            _view.PauseView.Hide();
        }

        private async void OnMainMenuClicked()
        {
            var confirmationSetupData = new ConfirmationSetupData()
            {
                HeaderText = "Exit", DescriptionText = "You sure you want to exit?"
            };

            //todo: сделать два метода в диалог сервисе, показ сингтон окна и показ мультипл инстанс окна
            _exitConfirmationDialog = await _dialogViewService.ShowAsync<ConfirmationDialogView>(confirmationSetupData);

            _exitConfirmationDialog.OnConfirmClicked += OnMainMenuExitConfirmed;
            _disposeActions.Add(() => { _exitConfirmationDialog.OnConfirmClicked -= OnMainMenuExitConfirmed; });
        }

        private void OnMainMenuExitConfirmed(bool confirmed)
        {
            _exitConfirmationDialog.HideAsync();

            if (confirmed)
            {
                _sectionSwitchService.Switch("MainMenu");
            }
        }

        private void CreateGame()
        {
            _fieldConstructor.CreateField(_gameplaySettings.FieldSize);

            var botInputStrategy = new BotInputStrategy(_gameplaySettings.BotStrategyInputModel,
                _fieldConstructor.FieldCellModels);

            var playerInputStrategy = new PlayerInputStrategy(_gameplaySettings.PlayerInputStrategyModel,
                _fieldConstructor.FieldCellModelsByView);
            playerInputStrategy.Initialize();
            _playerInputStrategy = playerInputStrategy;

            _inputStrategies.AddLast(botInputStrategy);
            _inputStrategies.AddLast(playerInputStrategy);

            foreach (var inputStrategy in _inputStrategies)
            {
                _roundsByInputStrategies.Add(inputStrategy, 0);
            }
        }

        private void StartRound()
        {
            foreach (var fieldCellModel in _fieldConstructor.FieldCellModels)
            {
                fieldCellModel.ClearClaim();
            }

            foreach (var fieldCellView in _fieldConstructor.FieldCellViews)
            {
                fieldCellView.ClearClaim();
            }

            var randomTurn = Random.Range(0, 2);

            if (randomTurn > 0)
            {
                _currentTurnInputStrategy = _inputStrategies.First;
            }
            else
            {
                _currentTurnInputStrategy = _inputStrategies.Last;
            }

            _currentTurnInputStrategy.Value.OnInput += SetTextTurn;
            _currentTurnInputStrategy.Value.HandleInput();

            _gameTimer.Start();
            _gameTimer.OnTick += _view.SetTime;
        }

        private void SetTextTurn(FieldCellModel fieldCellModel)
        {
            ClaimFieldCellBy(fieldCellModel, _currentTurnInputStrategy.Value);

            _currentTurnInputStrategy.Value.OnInput -= SetTextTurn;

            if (CheckLineWinLenght(fieldCellModel))
            {
                _roundsByInputStrategies[_currentTurnInputStrategy.Value]++;

                if (_roundsByInputStrategies[_currentTurnInputStrategy.Value] == _gameplaySettings.TotalRounds)
                {
                    EndGame(_currentTurnInputStrategy.Value);
                }
                else
                {
                    StartRound();
                }

                return;
            }

            _currentTurnInputStrategy = _currentTurnInputStrategy.Next;

            if (_currentTurnInputStrategy == null)
            {
                _currentTurnInputStrategy = _inputStrategies.First;
            }

            _currentTurnInputStrategy.Value.OnInput += SetTextTurn;
            _currentTurnInputStrategy.Value.HandleInput();
        }

        private async void EndGame(IInputStrategy winnerStrategy)
        {
            var oldScore = _saveDataService.GetData<int>("Score");
            var newScore = oldScore;
            _view.EndGameScreenView.Show();

            if (winnerStrategy == _playerInputStrategy)
            {
                newScore += _gameplaySettings.ScoreReward;
                _saveDataService.SetData("Score", newScore);
                await _view.EndGameScreenView.AddWinScore(oldScore, newScore, _gameplaySettings.ScoreReward);
            }
            else
            {
                newScore -= _gameplaySettings.ScoreReward;
                _saveDataService.SetData("Score", newScore);
                await _view.EndGameScreenView.AddLoseScore(oldScore, newScore, _gameplaySettings.ScoreReward);
            }

            var gameResult = new GameplayResult(winnerStrategy.Id, _gameTimer.CurrentTime);

            _sectionSwitchService.Switch("MainMenu", gameResult);
        }

        private void ClaimFieldCellBy(FieldCellModel fieldCellModel, IInputStrategy inputStrategy)
        {
            fieldCellModel.ClaimCell(inputStrategy.Id);

            var fieldCellView = _fieldConstructor.FieldCellViewsByModel[fieldCellModel];
            fieldCellView.SetClaimed(fieldCellModel.ClaimedById);
        }

        private bool CheckLineWinLenght(FieldCellModel fieldCellModel)
        {
            foreach (var lineWinDirection in _lineWinDirections)
            {
                var lineLenght = 1;

                var directions = new[]
                {
                    lineWinDirection,
                    -lineWinDirection
                };

                foreach (var direction in directions)
                {
                    var nextPosition = fieldCellModel.GridPosition + direction;
                    var nextCellField =
                        _fieldConstructor.FieldCellModels.FirstOrDefault(c => c.GridPosition == nextPosition);

                    while (nextCellField != null)
                    {
                        if (nextCellField.ClaimedById == fieldCellModel.ClaimedById)
                        {
                            lineLenght++;
                        }
                        else
                        {
                            break;
                        }

                        nextPosition += direction;
                        nextCellField =
                            _fieldConstructor.FieldCellModels.FirstOrDefault(c => c.GridPosition == nextPosition);
                    }
                }

                if (lineLenght >= _gameplaySettings.LineWinLenght)
                {
                    return true;
                }
            }

            return false;
        }
    }
}