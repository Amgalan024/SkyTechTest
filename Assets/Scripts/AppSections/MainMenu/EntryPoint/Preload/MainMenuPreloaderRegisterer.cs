﻿using Core.PreloadLogic;
using VContainer;

namespace AppSections.PreloadLogic
{
    /// <summary>
    /// Здесь в Configure будут регистрироваться зависимости GameplayPreloader'а,
    /// допустим ScriptableObject конфиг с путями тяжелых Addressable ассетов для их асинхронной загрузки + инстанциирования
    /// </summary>
    public class MainMenuPreloaderRegisterer : PreloaderRegisterer
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MainMenuPreloader>(Lifetime.Singleton);
        }
    }
}