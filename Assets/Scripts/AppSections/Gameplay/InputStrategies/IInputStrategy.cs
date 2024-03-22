using System;
using AppSections.Gameplay.Models;

namespace AppSections.Gameplay.InputStrategies
{
    public interface IInputStrategy
    {
        IInputStrategyModel Model { get; }
        event Action<FieldCellModel> OnInput;
        void HandleInput();
    }
}