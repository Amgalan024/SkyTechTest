using Cysharp.Threading.Tasks;
using Services.DialogView.Views;
using UnityEngine.Assertions;

namespace Services.DialogView
{
    public class DialogViewService
    {
        private readonly DialogViewProvider _dialogViewProvider;

        public DialogViewService(DialogViewProvider dialogViewProvider)
        {
            _dialogViewProvider = dialogViewProvider;
        }

        public async UniTask<TDialogView> ShowAsync<TDialogView>(object setupData) where TDialogView : BaseDialogView
        {
            var dialogView = _dialogViewProvider.GetDialogView<TDialogView>();

            Assert.IsNotNull(dialogView);

            dialogView.BaseSetup();
            dialogView.Setup(setupData);

            await dialogView.ShowAsync();

            return dialogView;
        }
    }
}