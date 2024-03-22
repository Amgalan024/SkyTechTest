using VContainer;
using VContainer.Unity;

namespace Core.PreloadLogic
{
    /// <summary>
    /// Выделенный класс для вызова метода подготовки\регистрации у IEntryPointPreloaderRegistration
    /// </summary>
    public class EntryPointPreloaderRegistrar : LifetimeScope
    {
        private IEntryPointPreloaderRegistration _entryPointPreloaderRegistration;

        protected override void Configure(IContainerBuilder builder)
        {
            _entryPointPreloaderRegistration.RegisterPreloader(builder);
        }

        public void SetEntryPointPreloaderRegistration(IEntryPointPreloaderRegistration entryPointPreloaderRegistration)
        {
            _entryPointPreloaderRegistration = entryPointPreloaderRegistration;
        }

        public T GetPreloader<T>() where T : IEntryPointPreloader
        {
            return Container.Resolve<T>();
        }
    }
}