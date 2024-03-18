using System;
using System.Collections.Generic;
using System.Linq;
using Core.Store.Configs;
using Core.Store.Models;
using Core.Store.Providers;
using Core.Store.View;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core.Store.Controller
{
    public class StoreController : IInitializable
    {
        private readonly IProductsProvider _productsProvider;
        private Products _products;
        private StoreConfig _config;
        private StoreView _storeView;

        private readonly Dictionary<Type, Type> _productTypeToProductViewDict = new()
        {
            {typeof(GameCurrencyProduct), typeof(GameCurrencyProductView)},
            {typeof(ItemPackProduct), typeof(ItemPackProductView)}
        };

        public StoreController(IProductsProvider productsProvider)
        {
            _productsProvider = productsProvider;
        }

        public async void Initialize()
        {
            _products = await _productsProvider.GetProducts();

            foreach (var shopItem in _products.shopItems)
            {
                var itemViewType = _productTypeToProductViewDict[shopItem.GetType()];

                var itemViewPrefab = _config.BaseItemViewPrefabs.FirstOrDefault(p => p.GetType() == itemViewType);

                var itemView = Object.Instantiate(itemViewPrefab, _storeView.InstantiateParent);

                _storeView.AddProductView(itemView);
            }
        }
    }
}