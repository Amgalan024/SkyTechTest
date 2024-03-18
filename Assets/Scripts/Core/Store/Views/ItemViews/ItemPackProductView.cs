using Core.Store.Models;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Store.View
{
    public class ItemPackProductView : BaseProductView
    {
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        public override void Setup(object setupData)
        {
            var productData = (ItemPackProduct) setupData;
            Assert.IsNotNull(productData);

            _headerText.text = productData.key;

            _descriptionText.text = "Items: \n";
            foreach (var item in productData.items)
            {
                _descriptionText.text += $"-{item.key} \n";
            }
        }
    }
}