using System;
using Cysharp.Threading.Tasks;
using Services.DialogView.SetupData;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Services.DialogView.Views
{
    public class NotificationDialogView : BaseDialogView
    {
        [SerializeField] private TextMeshProUGUI _notificationText;

        private float _hideDelay;

        public override void Setup(object setupData)
        {
            var notificationSetupData = (NotificationSetupData) setupData;
            Assert.IsNotNull(notificationSetupData);
            _notificationText.text = notificationSetupData.NotificationText;
            _hideDelay = notificationSetupData.Duration;
        }

        protected override async UniTask DoOnShowAsync()
        {
            gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(_hideDelay));
            await HideAsync();
        }

        protected async override UniTask DoOnHideAsync()
        {
            gameObject.SetActive(false);
        }
    }
}