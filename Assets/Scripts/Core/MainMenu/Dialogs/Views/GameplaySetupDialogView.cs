using System;
using Core.MainMenu.Models;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Utils.DialogView.Views;

namespace Core.MainMenu.Views.DialogView
{
    public class GameplaySetupDialogView : BaseDialogView
    {
        public event Action OnConfirmClicked;

        [SerializeField] private Slider _roundsCountSlider;
        [SerializeField] private Button _confirmButton;

        public int RoundsSliderValue => (int) _roundsCountSlider.value;

        public override void Setup(object setupData)
        {
            var gameplaySetupData = (GameplaySetupData) setupData;

            Assert.IsNotNull(gameplaySetupData);

            _roundsCountSlider.wholeNumbers = true;
            _roundsCountSlider.minValue = gameplaySetupData.MinRounds;
            _roundsCountSlider.maxValue = gameplaySetupData.MaxRounds;
            _roundsCountSlider.value = _roundsCountSlider.minValue;
        }

        public override async UniTask ShowAsync()
        {
            _confirmButton.onClick.AddListener(() =>
            {
                OnConfirmClicked?.Invoke();
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