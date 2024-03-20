using UnityEngine;
using VContainer;

namespace Services
{
    public abstract class BaseServiceBuilder : MonoBehaviour
    {
        public abstract object Build();
        public abstract void Configure(IContainerBuilder builder);
    }
}