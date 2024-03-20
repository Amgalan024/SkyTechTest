using System;
using Core.Gameplay.Models;

namespace Core.Gameplay.InputStrategies
{
    public interface IInputStrategy
    {
        string Id { get; }
        event Action<FieldCellModel> OnInput;
        void HandleInput();
    }
}