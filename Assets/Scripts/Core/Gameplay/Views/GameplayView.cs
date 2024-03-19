using TMPro;
using UnityEngine;

namespace Core.Gameplay.Views
{
    public class GameplayView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;

        public void SetName(string gameName)
        {
            _nameText.text = gameName;
        }
    }
}