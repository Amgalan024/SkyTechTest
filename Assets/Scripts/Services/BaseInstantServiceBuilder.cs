using UnityEngine;
using VContainer;

namespace Services
{
    public abstract class BaseInstantServiceBuilder : MonoBehaviour
    {
        public abstract object BuildService();
        public abstract void RegisterService(IContainerBuilder builder);
    }
}