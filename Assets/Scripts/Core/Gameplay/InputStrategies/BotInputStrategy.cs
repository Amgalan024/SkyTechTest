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
        public event Action<FieldCellModel> OnInput;

        private readonly List<FieldCellModel> _fieldCellModels;

        public BotInputStrategy(List<FieldCellModel> fieldCellModels)
        {
            _fieldCellModels = fieldCellModels;
        }

        public async void HandleInput()
        {
            //todo: сделать два отдельный списка для занятых ине занятых клеток, засунуть их в провайдер/контейнер какой то

            await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(1, 3))); //рандомная задержка перед решением бота
            var freeFieldCells = _fieldCellModels.Where(c => c.ClaimedById == null).ToList();

            var randomIndex = Random.Range(0, freeFieldCells.Count);

            var randomFieldCell = freeFieldCells[randomIndex];

            OnInput?.Invoke(randomFieldCell);
        }
    }
}