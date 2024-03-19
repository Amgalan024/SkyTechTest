using UnityEngine;

namespace Core
{
    public class EntryPointHolder : MonoBehaviour
    {
        [field: SerializeField] public BaseEntryPoint EntryPoint { get; private set; }
    }
}