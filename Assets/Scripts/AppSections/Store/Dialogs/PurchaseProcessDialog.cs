using AppSections.Store.Models;
using Cysharp.Threading.Tasks;
using Services.DialogView.Views;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace AppSections.Store.Dialogs
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

        protected override UniTask DoOnShowAsync()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        protected override UniTask DoOnHideAsync()
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