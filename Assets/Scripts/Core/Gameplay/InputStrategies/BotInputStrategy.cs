using System;
using System.Collections.Generic;
using System.Linq;
using Core.Gameplay.Models;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

namespace Core.Gameplay.InputStrategies
{
    public class BotInputStrategy : IInputStrategy
    {
        public IInputStrategyModel Model { get; }
        public event Action<FieldCellModel> OnInput;
        private readonly BotStrategyInputModel _model;
        private readonly List<FieldCellModel> _fieldCellModels;

        public BotInputStrategy(BotStrategyInputModel model, List<FieldCellModel> fieldCellModels)
        {
            Model = model;
            _model = model;
            _fieldCellModels = fieldCellModels;
        }

        public async void HandleInput()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(1, 3))); //рандомная задержка перед решением бота

            var freeFieldCells = _fieldCellModels.Where(c => c.IsClaimed == false).ToList();

            var randomIndex = Random.Range(0, freeFieldCells.Count);

            var randomFieldCell = freeFieldCells[randomIndex];

            OnInput?.Invoke(randomFieldCell);
        }
    }
}