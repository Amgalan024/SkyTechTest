using AppSections.Gameplay.Models;
using Cysharp.Threading.Tasks;
using Services.DialogView.Views;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace AppSections.MainMenu.Views.DialogView
{
    public class GameplayResultDialogView : BaseDialogView
    {
        [SerializeField] private TextMeshProUGUI _winnerText;

        public override void Setup(object setupData)
        {
            var resultData = (GameplayResult) setupData;

            Assert.IsNotNull(resultData);

            var timeString = resultData.GameDuration.ToString("mm:ss");

            _winnerText.text = $"Winner: {resultData.WinnerName}\n Time: {timeString}";
        }

        protected override UniTask DoOnShowAsync()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        protected override UniTask DoOnHideAsync()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}