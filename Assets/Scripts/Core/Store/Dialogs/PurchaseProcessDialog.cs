using Core.Store.Models;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using Services.DialogView.Views;

namespace Core.Store.Dialogs
{
    public class PurchaseProcessDialog : BaseDialogView
    {
        [SerializeField] private TextMeshProUGUI _description;

        public override void Setup(object setupData)
        {
            var productData = (BaseProduct) setupData;
            Assert.IsNotNull(productData);

            _description.text = $"Purchasing {productData.key}...";
        }

        public override UniTask ShowAsync()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public override UniTask HideAsync()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }

        public void SetPurchaseStatus(bool status)
        {
            _description.text = status ? "Purchased successfully" : "Purchase failed";
        }
    }
}