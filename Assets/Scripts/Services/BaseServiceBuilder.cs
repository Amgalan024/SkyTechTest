using UnityEngine;
using VContainer;

namespace Utils
{
    public abstract class BaseServiceBuilder : MonoBehaviour
    {
        public abstract void Build(IContainerBuilder builder);
    }
}