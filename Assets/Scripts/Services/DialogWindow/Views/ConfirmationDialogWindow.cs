using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Utils.DialogWindow.SetupData;

namespace Utils.DialogWindow.Views
{
    //Мб будет подходить под разновидность окна с выбором опций по кнопкам хз и придется убрать отдельный класс
    public class ConfirmationDialogWindow : BaseDialogWindow
    {
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        
        public override void Setup(object setupData, Action<object> onHide)
        {
            var confirmationSetupData = (ConfirmationSetupData) setupData;

            Assert.IsNotNull(confirmationSetupData);

            _headerText.text = confirmationSetupData.HeaderText;
            _descriptionText.text = confirmationSetupData.DescriptionText;

            _yesButton.onClick.AddListener(() =>
            {
                onHide?.Invoke(true);
                _yesButton.onClick.RemoveAllListeners();
                HideAsync().Forget();
            });

            _noButton.onClick.AddListener(() =>
            {
                onHide?.Invoke(false);
                _noButton.onClick.RemoveAllListeners();
                HideAsync().Forget();
            });
        }

        public override async UniTask ShowAsync()
        {
            gameObject.SetActive(true);
        }

        public override async UniTask HideAsync()
        {
            gameObject.SetActive(false);
        }
    }
}