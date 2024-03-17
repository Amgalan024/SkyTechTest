using System.Collections.Generic;
using UnityEngine.Assertions;
using Utils;

namespace SceneSwitchLogic.Switchers
{
    //todo:Подобрать другое название вместо Scene, тут происходит "переключения логических частей игры" 
    public class SceneSwitchService : IService
    {
        public bool Ready { get; }

        private readonly Dictionary<string, ISwitcher> _switchers = new();

        public void AddSwitcher(ISwitcher switcher)
        {
            _switchers.Add(switcher.Key, switcher);
        }

        public void AddSwitchers(IEnumerable<ISwitcher> switchers)
        {
            foreach (var switcher in switchers)
            {
                _switchers.Add(switcher.Key, switcher);
            }
        }

        public void Switch(string key)
        {
            _switchers.TryGetValue(key, out var switcher);

            Assert.IsNotNull(switcher);

            switcher.Switch();
        }
    }
}