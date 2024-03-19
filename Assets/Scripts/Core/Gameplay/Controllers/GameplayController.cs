using System;
using System.Collections.Generic;
using System.Linq;
using Core.Gameplay.InputStrategies;
using Core.Gameplay.Models;
using Core.Gameplay.Views;
using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using Services.SectionSwitchService;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer.Unity;

namespace Core.Gameplay.Controllers
{
    public class GameplayController : IInitializable
    {
        private readonly SectionSwitchParams _sectionSwitchParams;
        private readonly SectionSwitchService _sectionSwitchService;

        private readonly FieldConstructor _fieldConstructor;

        private readonly GameplayView _view;
        private GameplaySettings _gameplaySettings;

        private LinkedList<IInputStrategy> _inputStrategies;
        private LinkedListNode<IInputStrategy> _currentTurnInputStrategy;

        private readonly List<Vector2> _lineWinDirections = new() //todo: Вынести в конфиг
        {
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(-1, 1),
        };

        public GameplayController(SectionSwitchParams sectionSwitchParams, GameplayView view,
            SectionSwitchService sectionSwitchService, FieldConstructor fieldConstructor)
        {
            _sectionSwitchParams = sectionSwitchParams;
            _view = view;
            _sectionSwitchService = sectionSwitchService;
            _fieldConstructor = fieldConstructor;
        }

        void IInitializable.Initialize()
        {
            _gameplaySettings =
                (GameplaySettings) _sectionSwitchParams.SwitchParams.FirstOrDefault(p =>
                    p.GetType() == typeof(GameplaySettings));

            Assert.IsNotNull(_gameplaySettings);

            _view.SetPlayerName(_gameplaySettings.PlayerName);
            _view.SetOpponentName(_gameplaySettings.OpponentName);
            _view.SetRoundsCounterStatus(0, _gameplaySettings.TotalRounds);
            StartGameplay();
        }

        private void StartGameplay()
        {
            _fieldConstructor.CreateField(_gameplaySettings.FieldSize);

            //todo:сделать зависимость стратегий от настроек которые пришли с меню
            var botStrategy = new BotInputStrategy(_fieldConstructor.FieldCellModels);
            var playerStrategy = new PlayerInputStrategy(_fieldConstructor.FieldCellModelsByView);
            playerStrategy.Initialize();

            _inputStrategies.AddFirst(botStrategy);
            _inputStrategies.AddFirst(playerStrategy);

            _currentTurnInputStrategy = _inputStrategies.First; //todo: сделать рандом выбора первоого хода

            _currentTurnInputStrategy.Value.OnInput += SetTextTurn;
            _currentTurnInputStrategy.Value.HandleInput();
        }

        private async void SetTextTurn(FieldCellModel fieldCellModel)
        {
            ClaimFieldCell(fieldCellModel);

            _currentTurnInputStrategy.Value.OnInput -= SetTextTurn;

            if (CheckLineWinLenght(fieldCellModel))
            {
                await UniTask.Delay(TimeSpan
                    .FromSeconds(3)); //todo: добавить анимацию победы с показом очков и чет там еще по ТЗ

                var gameResult = new GameplayResult(fieldCellModel.ClaimedById);

                _sectionSwitchService.Switch("MainMenu", gameResult);

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

        private void ClaimFieldCell(FieldCellModel fieldCellModel)
        {
            var fieldCellView = _fieldConstructor.FieldCellViewsByModel[fieldCellModel];
            fieldCellView.SetClaimed(fieldCellModel.ClaimedById);
        }

        private bool CheckLineWinLenght(FieldCellModel fieldCellModel)
        {
            foreach (var lineWinDirection in _lineWinDirections)
            {
                //todo: отрефакторить убрать копипаст выделить в методы
                var lineLenght = 0;

                var directions = new[]
                {
                    lineWinDirection,
                    -lineWinDirection
                };

                foreach (var direction in directions)
                {
                    var newPosition = fieldCellModel.GridPosition + direction;
                    var newCellField =
                        _fieldConstructor.FieldCellModels.FirstOrDefault(c => c.GridPosition == newPosition);

                    while (newCellField != null)
                    {
                        newPosition += direction;
                        newCellField =
                            _fieldConstructor.FieldCellModels.FirstOrDefault(c => c.GridPosition == newPosition);
                        lineLenght++;
                    }
                }

                // //Подсчет длины линии в направлении
                // var newPosition = fieldCellModel.GridPosition + winDirection;
                // var newCellField = _fieldConstructor.FieldCellModels.FirstOrDefault(c => c.GridPosition == newPosition);
                //
                // while (newCellField != null)
                // {
                //     newPosition += winDirection;
                //     newCellField = _fieldConstructor.FieldCellModels.FirstOrDefault(c => c.GridPosition == newPosition);
                //     lineLenght++;
                // }
                //
                // //Подсчет длины линии в противоположном направлении
                // newPosition = fieldCellModel.GridPosition - winDirection;
                // newCellField = _fieldConstructor.FieldCellModels.FirstOrDefault(c => c.GridPosition == newPosition);
                //
                // while (newCellField != null)
                // {
                //     newPosition -= winDirection;
                //     newCellField = _fieldConstructor.FieldCellModels.FirstOrDefault(c => c.GridPosition == newPosition);
                //     lineLenght++;
                // }

                if (lineLenght >= _gameplaySettings.LineWinLenght)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckInFieldBounds(Vector2 gridPosition, int fieldSize)
        {
            var xInBounds = gridPosition.x >= 0 && gridPosition.x >= fieldSize;
            var yInBounds = gridPosition.y >= 0 && gridPosition.y >= fieldSize;
            return xInBounds && yInBounds;
        }
    }
}