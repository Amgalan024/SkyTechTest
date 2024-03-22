using System;
using AppSections.Shared.Configs;
using Core.PreloadLogic;
using UnityEngine;
using VContainer;

namespace AppSections.PreloadLogic
{
    /// <summary>
    /// Здесь в Configure будут регистрироваться зависимости GameplayPreloader'а,
    /// допустим ScriptableObject конфиг с путями тяжелых Addressable ассетов для их асинхронной загрузки + инстанциирования
    /// </summary>
    [Serializable]
    public class GameplayPreloaderRegistration : IEntryPointPreloaderRegistration
    {
        [SerializeField] private LoadDelayConfig _loadDelayConfig;
        
        public void RegisterPreloader(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadDelayConfig);
            builder.Register<GameplayPreloader>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}