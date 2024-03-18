using Core.Store.Controller;
using Core.Store.Providers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Store.EntryPoint
{
    public class StoreEntryPoint : BaseEntryPoint
    {
        //todo: Вынести в конфиг
        [SerializeField] private TextAsset _products;
        public override int LoadStepsCount { get; }

        public override async UniTask PreloadEntryPoint()
        {
            //В будущем инициализация удаленного провайдера покупок
        }

        public override void BuildEntryPoint()
        {
            Build();
        }

        protected override void Configure(IContainerBuilder builder)
        {
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
                .WithParameter("productsJson", _products.text);
        }
    }
}