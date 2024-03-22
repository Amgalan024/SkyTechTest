using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Services
{
    public abstract class BaseServiceBuilderWithSetup : MonoBehaviour
    {
        public abstract UniTask Setup();
        public abstract object BuildService();
        public abstract void RegisterService(IContainerBuilder builder);
    }
}