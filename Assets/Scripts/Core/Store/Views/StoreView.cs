using UnityEngine;
using UnityEngine.UI;

namespace Core.Store.View
{
    public class StoreView : MonoBehaviour
    {
        [field: SerializeField] public Transform InstantiateParent { get; private set; }

        [SerializeField] private LayoutGroup _layoutGroup;

        public void AddProductView(BaseProductView productView)
        {
            productView.transform.SetParent(_layoutGroup.transform);
        }
    }
}