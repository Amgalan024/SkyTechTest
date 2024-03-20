using Cysharp.Threading.Tasks;
using UnityEngine.Assertions;
using Utils.DialogView.Views;

namespace Utils.DialogView
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