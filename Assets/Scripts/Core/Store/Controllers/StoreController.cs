using System;
using System.Collections.Generic;
using System.Linq;
using Core.Store.Configs;
using Core.Store.Dialogs;
using Core.Store.Models;
using Core.Store.Providers;
using Core.Store.Views;
using Cysharp.Threading.Tasks;
using Services.DialogView;
using VContainer.Unity;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Core.Store.Controllers
{
    /// <summary>
    /// В будущем магазин можно удобно расширять различными видами покупок, создавая для этого новые модели данных наследуемые от BaseProduct
    /// и создавая для них соответствующие вьюшки которые будут эти модели данных принимать. 
    /// </summary>
    public class StoreController : IInitializable, IDisposable
    {
        private readonly IProductsProvider _productsProvider;
        private readonly StoreConfig _config;
        private readonly StoreView _storeView;
        private readonly DialogViewService _dialogViewService;

        private readonly Dictionary<Type, Type> _productTypeToProductViewDict = new()
        {
            {typeof(GameCurrencyProduct), typeof(GameCurrencyProductView)},
            {typeof(ItemPackProduct), typeof(ItemPackProductView)}
        };

        private Products _products;

        private readonly Dictionary<BaseProductView, BaseProduct> _productsByView = new();

        public StoreController(IProductsProvider productsProvider, StoreConfig config, StoreView storeView,
            DialogViewService dialogViewService)
        {
            _productsProvider = productsProvider;
            _config = config;
            _storeView = storeView;
            _dialogViewService = dialogViewService;
        }

        async void IInitializable.Initialize()
        {
            _products = await _productsProvider.GetProducts();

            foreach (var shopItem in _products.shopItems)
            {
                var productViewType = _productTypeToProductViewDict[shopItem.GetType()];

                var productViewPrefab = _config.BaseItemViewPrefabs.FirstOrDefault(p => p.GetType() == productViewType);

                var productView = Object.Instantiate(productViewPrefab, _storeView.ProductLayoutGroup.transform);
                productView.Setup(shopItem);
                productView.OnPurchaseClicked += HandlePurchase;

                _productsByView.Add(productView, shopItem);
            }
        }

        void IDisposable.Dispose()
        {
            foreach (var productView in _productsByView.Keys)
            {
                productView.OnPurchaseClicked -= HandlePurchase;
            }
        }

        private async void HandlePurchase(BaseProductView productView)
        {
            var product = _productsByView[productView];

            var purchaseDialog = await _dialogViewService.ShowAsync<PurchaseProcessDialog>(product);

            var randomPurchaseDelay = Random.Range(1, 3);

            await UniTask.Delay(TimeSpan.FromSeconds(randomPurchaseDelay));

            var randomPurchaseStatus = Random.Range(0, 2) == 1;

            purchaseDialog.SetPurchaseStatus(randomPurchaseStatus);
        }
    }
}