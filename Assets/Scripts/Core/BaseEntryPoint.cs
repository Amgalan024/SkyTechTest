using Services.SectionSwitchService;
using VContainer.Unity;

namespace Core
{
    /// <summary>
    /// Передача данных между разделами приложения идет через регистрацию SectionSwitchParams, пример в BaseSectionSwitcher,
    /// где в свитчер передается список параметров который записывается в SectionSwitchParams
    /// (отдельный класс SectionSwitchParams сделан для удобства регистрации, удобнее регать класс чем списов объектов который может конфликтовать
    /// если мы зарегаем другой список объектов связанный с совершенно другой логикой)
    /// </summary>
    public abstract class BaseEntryPoint : LifetimeScope
    {
        public SectionSwitchParams SectionSwitchParams { get; } = new();

        public abstract void BuildEntryPoint();
    }
}