using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Gameplay.Views
{
    public class FieldCellView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<FieldCellView> OnClicked;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TextMeshPro _idText;

        /// <summary>
        /// В будущем скорее всего сюда будет передаваться спрайт фигуры для заполнения клетки,
        /// спарйт будет выбираться в зависимости от игрока в контроллере по словарю например
        /// </summary>
        /// <param name="id"></param>
        public void SetClaimed(string id)
        {
            _idText.text = id;
        }

        public void SetSize(int size)
        {
            _spriteRenderer.size = Vector2.one * size;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(this);
        }
    }
}