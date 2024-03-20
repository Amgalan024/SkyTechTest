﻿using System;
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
            var botStrategy = new BotInputStrategy("1", _fieldConstructor.FieldCellModels);
            var playerStrategy = new PlayerInputStrategy("2", _fieldConstructor.FieldCellModelsByView);
            playerStrategy.Initialize();

            _inputStrategies = new LinkedList<IInputStrategy>();
            _inputStrategies.AddFirst(botStrategy);
            _inputStrategies.AddFirst(playerStrategy);

            _currentTurnInputStrategy = _inputStrategies.First; //todo: сделать рандом выбора первоого хода

            _currentTurnInputStrategy.Value.OnInput += SetTextTurn;
            _currentTurnInputStrategy.Value.HandleInput();
        }

        private async void SetTextTurn(FieldCellModel fieldCellModel)
        {
            ClaimFieldCellView(fieldCellModel);

            _currentTurnInputStrategy.Value.OnInput -= SetTextTurn;

            if (CheckLineWinLenght(fieldCellModel))
            {
                await UniTask.Delay(TimeSpan
                    .FromSeconds(1)); //todo: добавить анимацию победы с показом очков и чет там еще по ТЗ

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

        private void ClaimFieldCellView(FieldCellModel fieldCellModel)
        {
            var fieldCellView = _fieldConstructor.FieldCellViewsByModel[fieldCellModel];
            fieldCellView.SetClaimed(fieldCellModel.ClaimedById); //todo:в будущем во вьюшку пойдет не id а какой нить спрайт доделать
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