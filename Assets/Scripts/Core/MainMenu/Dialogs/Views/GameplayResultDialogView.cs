using System;
using Core.Gameplay.Models;
using Cysharp.Threading.Tasks;
using Services.DialogView.Views;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.MainMenu.Views.DialogView
{
    public class GameplayResultDialogView : BaseDialogView
    {
        [SerializeField] private TextMeshProUGUI _winnerText;

        public override void Setup(object setupData)
        {
            var resultData = (GameplayResult) setupData;

            Assert.IsNotNull(resultData);

            var timeString = new DateTime().AddSeconds(resultData.GameDuration).ToString("mm:ss");

            _winnerText.text = $"Winner: {resultData.WinnerName}\n Time: {timeString}";
        }

        public override UniTask ShowAsync()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public override UniTask HideAsync()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}