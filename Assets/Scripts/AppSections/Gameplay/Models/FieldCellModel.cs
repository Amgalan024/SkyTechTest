using System;
using UnityEngine;

namespace AppSections.Gameplay.Models
{
    public class FieldCellModel
    {
        public event Action<FieldCellModel> OnClaimed;

        public string ClaimedById { get; private set; }
        public bool IsClaimed => ClaimedById != string.Empty;
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

        public void ClearClaim()
        {
            ClaimedById = string.Empty;
        }
    }
}