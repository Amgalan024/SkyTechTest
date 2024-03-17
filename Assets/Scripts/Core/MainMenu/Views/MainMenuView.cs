﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.MainMenu.Views
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action OnStartClicked;
        public event Action OnStoreClicked;

        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _storeButton;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(() => { OnStartClicked?.Invoke(); });
            _storeButton.onClick.AddListener(() => { OnStoreClicked?.Invoke(); });
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