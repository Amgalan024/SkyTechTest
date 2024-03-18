using UnityEngine;

namespace Core
{
    public class EntryPointHolder : MonoBehaviour
    {
        [SerializeField] private GameObject _entryPointHolder;

        private BaseEntryPoint _entryPoint;

        public BaseEntryPoint GetEntryPoint()
        {
            if (_entryPoint == null)
            {
                _entryPoint = _entryPointHolder.GetComponent<BaseEntryPoint>();
            }

            return _entryPoint;
        }
    }
}