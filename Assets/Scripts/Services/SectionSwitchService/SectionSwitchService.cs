using System.Collections.Generic;
using UnityEngine.Assertions;

namespace SceneSwitchLogic.Switchers
{
    public class SectionSwitchService
    {
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