using AppSections.Store.Configs;
using AppSections.Store.Controllers;
using AppSections.Store.Providers;
using AppSections.Store.Views;
using Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AppSections.Store.EntryPoint
{
    public class StoreEntryPoint : BaseEntryPoint
    {
        [SerializeField] private StoreConfig _config;
        [SerializeField] private StoreView _view;
        
        public override void BuildEntryPoint()
        {
            Build();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(SectionSwitchParams);

            builder.RegisterInstance(_config);
            builder.RegisterInstance(_view);

            ConfigureLocalProductsProvider(builder);

            builder.RegisterEntryPoint<StoreController>();
        }

        /// <summary>
        /// Пока что покупки располагаются локально в проекте, в дальнейшем скорее всего покупки будут браться с сервера
        /// удаленно и будет подставляться RemoteProductsProvider, так же эти провайдеры могут взаимозаменяться в зависимости от наличия интернета
        /// поэтому выделен интерфейс
        /// </summary>
        /// <param name="builder"></param>
        private void ConfigureLocalProductsProvider(IContainerBuilder builder)
        {
            builder.Register<LocalProductsProvider>(Lifetime.Singleton).AsImplementedInterfaces()
                .WithParameter("productsJson", _config.Products.text);
        }
    }
}