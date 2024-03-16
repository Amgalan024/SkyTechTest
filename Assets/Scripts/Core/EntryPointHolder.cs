using UnityEngine;

namespace Core
{
    public class EntryPointHolder : MonoBehaviour
    {
        [SerializeField] private GameObject _entryPointHolder;

        private IEntryPoint _entryPoint;

        public IEntryPoint GetEntryPoint()
        {
            if (_entryPoint == null)
            {
                _entryPoint = _entryPointHolder.GetComponent<IEntryPoint>();
            }

            return _entryPoint;
        }
    }
}