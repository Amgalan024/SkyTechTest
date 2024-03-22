using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Services
{
    /// <summary>
    /// Точка входа для сервисов может расширяться любыми новыми сервисами которые будут прокинуты через сервис билдер в массив _baseServiceBuilders
    /// </summary>
    public class ServicesProvider : MonoBehaviour
    {
        [SerializeField] private BaseInstantServiceBuilder[] _instantServiceBuilders;
        [SerializeField] private BaseServiceBuilderWithSetup[] _serviceBuildersWithSetup;

        private readonly List<object> _services = new();

        public void BuildInstantServices()
        {
            foreach (var serviceBuilder in _instantServiceBuilders)
            {
                _services.Add(serviceBuilder.BuildService());
            }
        }

        public async UniTask BuildServicesWithSetup()
        {
            var taskList = new List<UniTask>();

            foreach (var serviceBuilder in _serviceBuildersWithSetup)
            {
                var task = serviceBuilder.Setup().ContinueWith(() => { _services.Add(serviceBuilder.BuildService()); });
                taskList.Add(task);
            }

            await UniTask.WhenAll(taskList);
        }

        public void ConfigureServices(IContainerBuilder builder)
        {
            foreach (var serviceBuilder in _instantServiceBuilders)
            {
                serviceBuilder.RegisterService(builder);
            }

            foreach (var serviceBuilder in _serviceBuildersWithSetup)
            {
                serviceBuilder.RegisterService(builder);
            }
        }

        public T GetService<T>()
        {
            var service = _services.FirstOrDefault(s => s.GetType() == typeof(T));
            return (T) service;
        }
    }
}