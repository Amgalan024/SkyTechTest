using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Store.Views
{
    public abstract class BaseProductView : MonoBehaviour
    {
        public event Action<BaseProductView> OnPurchaseClicked;

        [SerializeField] private Button _purchaseButton;

        public abstract void Setup(object productData);

        private void OnEnable()
        {
            _purchaseButton.onClick.AddListener(() => { OnPurchaseClicked?.Invoke(this); });
        }

        private void OnDisable()
        {
            _purchaseButton.onClick.RemoveAllListeners();
        }
    }
}