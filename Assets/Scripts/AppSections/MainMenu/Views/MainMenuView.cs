﻿using System;
using AppSections.Store.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AppSections.MainMenu.Views
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action OnStartClicked;
        public event Action OnStoreClicked;

        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _storeButton;
        [SerializeField] private TextMeshProUGUI _scoreText;

        [SerializeField] private StoreView _storeView;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(() => { OnStartClicked?.Invoke(); });
            _storeButton.onClick.AddListener(() => { OnStoreClicked?.Invoke(); });
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _storeButton.onClick.RemoveAllListeners();
        }

        public void SetScoreText(int score)
        {
            _scoreText.text = $"Your score: {score}";
        }

        public void OpenStore()
        {
            _storeView.Show();
        }
    }
}