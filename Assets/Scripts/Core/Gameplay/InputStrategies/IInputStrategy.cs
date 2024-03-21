using System;
using Core.Gameplay.Models;

namespace Core.Gameplay.InputStrategies
{
    public interface IInputStrategy
    {
        IInputStrategyModel Model { get; }
        event Action<FieldCellModel> OnInput;
        void HandleInput();
    }
}