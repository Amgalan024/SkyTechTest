using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Services;
using VContainer;

public class ServicesEntryPoint : MonoBehaviour
{
    [SerializeField] private BaseServiceBuilder[] _baseServiceBuilders;

    public bool ServicesReady { get; private set; }

    private readonly List<object> _services = new();

    public void BuildServices()
    {
        var taskList = new List<UniTask>();

        foreach (var serviceBuilder in _baseServiceBuilders)
        {
            switch (serviceBuilder)
            {
                case IWithSetup builderWithSetup:
                    var task = builderWithSetup.Setup().ContinueWith(() => { _services.Add(serviceBuilder.Build()); });
                    taskList.Add(task);
                    break;
                default:
                    _services.Add(serviceBuilder.Build());
                    break;
            }
        }

        if (taskList.Count > 0)
        {
            UniTask.WhenAll(taskList).ContinueWith(() => ServicesReady = true);
        }
        else
        {
            ServicesReady = true;
        }
    }

    public void ConfigureServices(IContainerBuilder builder)
    {
        foreach (var serviceBuilder in _baseServiceBuilders)
        {
            serviceBuilder.Configure(builder);
        }
    }

    public T GetService<T>()
    {
        var service = _services.FirstOrDefault(s => s.GetType() == typeof(T));
        return (T) service;
    }
}