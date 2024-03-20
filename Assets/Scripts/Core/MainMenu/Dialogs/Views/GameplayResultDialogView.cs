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

            _winnerText.text = resultData.WinnerName;
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