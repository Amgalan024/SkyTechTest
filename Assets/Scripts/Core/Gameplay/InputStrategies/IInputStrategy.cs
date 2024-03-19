using System;
using Core.Gameplay.Models;

namespace Core.Gameplay.InputStrategies
{
    public interface IInputStrategy
    {
        event Action<FieldCellModel> OnInput;
        void HandleInput();
    }
}