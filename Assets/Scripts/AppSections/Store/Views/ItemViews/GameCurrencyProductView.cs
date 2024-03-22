using AppSections.Store.Models;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace AppSections.Store.Views
{
    public class GameCurrencyProductView : BaseProductView
    {
        [SerializeField] private TextMeshProUGUI _header;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _costText;

        public override void Setup(object setupData)
        {
            var productData = (GameCurrencyProduct) setupData;

            Assert.IsNotNull(productData);

            _header.text = productData.key;
            _description.text = $"Currency: {productData.key} \nAmount :{productData.amount}";
            _costText.text = $"Cost: {productData.price} {productData.currency}";
        }
    }
}