using UnityEngine;

namespace Core.Store.View
{
    public abstract class BaseProductView : MonoBehaviour
    {
        public abstract void Setup(object productData);
    }
}