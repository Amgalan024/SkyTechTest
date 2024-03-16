using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Utils.DialogView.SetupData;

namespace Utils.DialogView.Views
{
    //Мб будет подходить под разновидность окна с выбором опций по кнопкам хз и придется убрать отдельный класс
    public class ConfirmationDialogView : BaseDialogView
    {
        public event Action<bool> OnConfirmClicked;

        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;

        public override void Setup(object setupData)
        {
            var confirmationSetupData = (ConfirmationSetupData) setupData;

            Assert.IsNotNull(confirmationSetupData);

            _headerText.text = confirmationSetupData.HeaderText;
            _descriptionText.text = confirmationSetupData.DescriptionText;
        }

        public override async UniTask ShowAsync()
        {
            _yesButton.onClick.AddListener(() => { OnConfirmClicked?.Invoke(true); });

            _noButton.onClick.AddListener(() => { OnConfirmClicked?.Invoke(false); });

            gameObject.SetActive(true);
        }

        public override async UniTask HideAsync()
        {
            _yesButton.onClick.RemoveAllListeners();
            _noButton.onClick.RemoveAllListeners();

            gameObject.SetActive(false);
        }
    }
}