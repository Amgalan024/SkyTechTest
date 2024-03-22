using UnityEngine;
using UnityEngine.UI;

namespace AppSections.Store.Views
{
    public class StoreView : MonoBehaviour
    {
        [field: SerializeField] public LayoutGroup ProductLayoutGroup { get; private set; }

        [SerializeField] private Button _closeButton;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Hide);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}