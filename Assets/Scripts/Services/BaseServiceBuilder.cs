using UnityEngine;
using VContainer;

namespace Utils
{
    public abstract class BaseServiceBuilder : MonoBehaviour
    {
        public abstract IService Build();
        public abstract void Configure(IContainerBuilder builder);
    }
}