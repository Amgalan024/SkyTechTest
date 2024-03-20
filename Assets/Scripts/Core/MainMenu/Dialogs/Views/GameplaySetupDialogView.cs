﻿using System;
using Core.MainMenu.Models;
using Cysharp.Threading.Tasks;
using Services.DialogView.Views;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Core.MainMenu.Views.DialogView
{
    public class GameplaySetupDialogView : BaseDialogView
    {
        public event Action OnConfirmClicked;

        [SerializeField] private IntValueSetupSlider _roundsCountSlider;
        [SerializeField] private IntValueSetupSlider _fieldSizeSlider;
        [SerializeField] private IntValueSetupSlider _lineWinLenghtSlider;
        [SerializeField] private Button _confirmButton;

        public int RoundsSliderValue => _roundsCountSlider.Value;
        public int FieldSizeSliderValue => _fieldSizeSlider.Value;
        public int LineWinLenghtSliderValue => _lineWinLenghtSlider.Value;

        public override void Setup(object setupData)
        {
            var gameplaySetupData = (GameplaySetupSettingsData) setupData;

            Assert.IsNotNull(gameplaySetupData);

            _roundsCountSlider.Setup(gameplaySetupData.TotalRoundsSetupName, gameplaySetupData.MinRounds,
                gameplaySetupData.MaxRounds, gameplaySetupData.MinRounds);
            _fieldSizeSlider.Setup(gameplaySetupData.FieldSizeSetupName, gameplaySetupData.MinFieldSize,
                gameplaySetupData.MaxFieldSize, gameplaySetupData.MinFieldSize);
            _lineWinLenghtSlider.Setup(gameplaySetupData.LineWinLeghtSetupName, gameplaySetupData.MinFieldSize,
                gameplaySetupData.MinFieldSize, gameplaySetupData.MinFieldSize);

            _fieldSizeSlider.OnValueChanged += OnFieldSizeSliderValueChanged;
        }

        public override async UniTask ShowAsync()
        {
            _confirmButton.onClick.AddListener(() =>
            {
                OnConfirmClicked?.Invoke();
                HideAsync();
            });
            gameObject.SetActive(true);
        }

        public override async UniTask HideAsync()
        {
            _confirmButton.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }

        private void OnFieldSizeSliderValueChanged(int value)
        {
            var currentValue = _lineWinLenghtSlider.Value <= value ? _lineWinLenghtSlider.Value : value;

            _lineWinLenghtSlider.SetValues(_fieldSizeSlider.MinValue, value, currentValue);
        }
    }
}