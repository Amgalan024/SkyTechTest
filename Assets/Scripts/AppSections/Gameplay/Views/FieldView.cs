using UnityEngine;

namespace AppSections.Gameplay.Views
{
    public class FieldView : MonoBehaviour
    {
        [field: SerializeField] public int Size { get; private set; }
        [field: SerializeField] public Transform CenterPoint { get; private set; }
        [field: SerializeField] public Transform CellsContainer { get; private set; }

        [SerializeField] private SpriteRenderer _borderSprite;

        private void Start()
        {
            _borderSprite.size = Vector2.one * Size;
        }
    }
}