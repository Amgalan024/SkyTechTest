using System;
using System.Collections.Generic;
using System.Linq;
using Core.Store.Models;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Store.Providers
{
    public class LocalProductsProvider : IProductsProvider
    {
        private readonly string _productsJson;
        private Products _products;

        private readonly List<Type> _productTypes = new()
        {
            typeof(GameCurrencyProduct),
            typeof(ItemPackProduct)
        };

        public LocalProductsProvider(string productsJson)
        {
            _productsJson = productsJson;
        }

        public UniTask<Products> GetProducts()
        {
            _products = GetProductsVar1();
            _products = GetProductsVar2();

            return new UniTask<Products>(_products);
        }

        /// <summary>
        /// Изначальный Json файл покупок с ТЗ сложно десериализовать без указания типов в самом Json файле,
        /// данная десериализация предназначена для Json с указанными типами покупок в списке shopItems
        /// </summary>
        /// <returns></returns>
        private Products GetProductsVar1()
        {
            var jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            var products = JsonConvert.DeserializeObject<Products>(_productsJson, jsonSettings);

            return products;
        }

        /// <summary>
        /// Десериализация изначального Json файла покупок без указания в нем типов
        /// </summary>
        /// <returns></returns>
        private Products GetProductsVar2()
        {
            var products = new Products
            {
                shopItems = new List<BaseProduct>()
            };

            var jObject = JObject.Parse(_productsJson);
            var shopItemsJToken = jObject.GetValue(nameof(products.shopItems));
            var shopItemsJTokens = shopItemsJToken.Children().ToList();

            foreach (var shopItemToken in shopItemsJTokens)
            {
                foreach (var type in _productTypes)
                {
                    if (IsCorrespondedJsonToType(shopItemToken.ToString(), type))
                    {
                        var product = shopItemToken.ToObject(type);
                        products.shopItems.Add((BaseProduct) product);
                    }
                }
            }

            return products;
        }

        private bool IsCorrespondedJsonToType(string json, Type type)
        {
            var jObject = JObject.Parse(json);

            var fields = type.GetFields();
            bool isCorresponded = true;

            foreach (var field in fields)
            {
                if (jObject.GetValue(field.Name) == null)
                {
                    isCorresponded = false;
                    break;
                }
            }

            return isCorresponded;
        }
    }
}