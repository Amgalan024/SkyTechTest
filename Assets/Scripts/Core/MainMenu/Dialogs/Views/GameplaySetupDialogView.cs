using System;
using Core.MainMenu.Models;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Utils.DialogView.Views;

namespace Core.MainMenu.Views.DialogView
{
    public class GameplaySetupDialogView : BaseDialogView
    {
        public event Action OnConfirmClicked;

        [SerializeField] private TextMeshProUGUI _selectedRoundsCountText;
        [SerializeField] private TextMeshProUGUI _minRoundsCountText;
        [SerializeField] private TextMeshProUGUI _maxRoundsCountText;
        [SerializeField] private Slider _roundsCountSlider;
        [SerializeField] private Button _confirmButton;

        public int RoundsSliderValue => (int) _roundsCountSlider.value;

        public override void Setup(object setupData)
        {
            var gameplaySetupData = (GameplaySetupSettingsData) setupData;

            Assert.IsNotNull(gameplaySetupData);

            _roundsCountSlider.wholeNumbers = true;
            _roundsCountSlider.minValue = gameplaySetupData.MinRounds;
            _roundsCountSlider.maxValue = gameplaySetupData.MaxRounds;
            _roundsCountSlider.value = _roundsCountSlider.minValue;

            _minRoundsCountText.text = gameplaySetupData.MinRounds.ToString();
            _maxRoundsCountText.text = gameplaySetupData.MaxRounds.ToString();
            _roundsCountSlider.onValueChanged.AddListener(value => _selectedRoundsCountText.text = ((int)value).ToString());
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
    }
}