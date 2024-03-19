using System;
using UnityEngine;

namespace Core.Gameplay.Models
{
    public class FieldCellModel
    {
        public event Action<FieldCellModel> OnClaimed;

        public string ClaimedById { get; private set; }
        public Vector2 GridPosition { get; }

        public FieldCellModel(Vector2 gridPosition)
        {
            GridPosition = gridPosition;
        }

        public void ClaimCell(string id)
        {
            ClaimedById = id;
            OnClaimed?.Invoke(this);
        }
    }
}