using UnityEngine;

namespace Core.Gameplay.Views
{
    public class FieldView : MonoBehaviour
    {
        [field: SerializeField] public int Size { get; private set; }
        [field: SerializeField] public Transform CenterPoint { get; private set; }
        [field: SerializeField] public Transform CellsContainer { get; private set; }
    }
}