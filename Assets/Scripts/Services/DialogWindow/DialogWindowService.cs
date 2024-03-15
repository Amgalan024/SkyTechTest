using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Assertions;
using Utils.DialogWindow.Views;

namespace Utils.DialogWindow
{
    public class DialogWindowService
    {
        private readonly DialogWindowProvider _dialogWindowProvider;

        public DialogWindowService(DialogWindowProvider dialogWindowProvider)
        {
            _dialogWindowProvider = dialogWindowProvider;
        }

        public async UniTask ShowAsync<TDialogWindow>(object setupData, Action<object> onHide) where TDialogWindow : BaseDialogWindow
        {
            var dialogWindow = _dialogWindowProvider.GetDialogWindow<TDialogWindow>();

            Assert.IsNotNull(dialogWindow);

            dialogWindow.Setup(setupData, onHide);
            await dialogWindow.ShowAsync();
        }
    }
}