using System.Collections.Generic;
using UnityEngine.Assertions;
using Utils;

namespace SceneSwitchLogic.Switchers
{
    public class SectionSwitchService : IService
    {
        public bool Ready { get; }

        private readonly Dictionary<string, ISectionSwitcher> _switchers = new();

        public void AddSwitcher(ISectionSwitcher sectionSwitcher)
        {
            _switchers.Add(sectionSwitcher.Key, sectionSwitcher);
        }

        public void AddSwitchers(IEnumerable<ISectionSwitcher> switchers)
        {
            foreach (var switcher in switchers)
            {
                _switchers.Add(switcher.Key, switcher);
            }
        }

        public void Switch(string key, params object[] switchParams)
        {
            _switchers.TryGetValue(key, out var switcher);

            Assert.IsNotNull(switcher);

            switcher.Switch(switchParams);
        }
    }
}