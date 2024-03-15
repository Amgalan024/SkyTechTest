using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.MainMenu.View
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action StartPressed;
        public event Action StorePressed;

        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _storeButton;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(() => { StartPressed?.Invoke(); });
            _storeButton.onClick.AddListener(() => { StorePressed?.Invoke(); });
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _storeButton.onClick.RemoveAllListeners();
        }
        
        public void SetScoreText(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}