﻿using AppSections.Store.Models;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace AppSections.Store.Views
{
    public class ItemPackProductView : BaseProductView
    {
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _costText;

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

            _costText.text = $"Cost: {productData.price} {productData.currency}";
        }
    }
}