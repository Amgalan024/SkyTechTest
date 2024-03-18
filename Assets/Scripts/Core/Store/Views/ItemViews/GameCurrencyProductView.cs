using Core.Store.Models;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Store.View
{
    public class GameCurrencyProductView : BaseProductView
    {
        [SerializeField] private TextMeshProUGUI _header;
        [SerializeField] private TextMeshProUGUI _description;

        public override void Setup(object setupData)
        {
            var productData = (GameCurrencyProduct) setupData;

            Assert.IsNotNull(productData);

            //todo: Придумать иной способ брать названия, сделать отдельный конфиг
            //с подробным описанием покупок, соотносить по ключу и брать нужные данные(имя, подробное описание, иконка и т.д) 
            _header.text = productData.key;
            _description.text = $"Currency: {productData.key} \nAmount :{productData.amount}";
        }
    }
}