using System;
using AppSections.MainMenu.Models;
using Cysharp.Threading.Tasks;
using Services.DialogView.Views;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace AppSections.MainMenu.Views.DialogView
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

        protected override async UniTask DoOnShowAsync()
        {
            _confirmButton.onClick.AddListener(() =>
            {
                OnConfirmClicked?.Invoke();
                DoOnHideAsync();
            });
            gameObject.SetActive(true);
        }

        protected override async UniTask DoOnHideAsync()
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