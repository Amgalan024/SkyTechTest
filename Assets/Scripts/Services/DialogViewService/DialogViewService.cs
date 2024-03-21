using Cysharp.Threading.Tasks;
using Services.DialogView.Views;
using UnityEngine.Assertions;

namespace Services.DialogView
{
    /// <summary>
    /// Данный сервис будет расширяться за счет воздания новых наследников BaseDialogView с различными параметрами для настройки,
    /// сейчас есть диалоговые окна с потверждением, настройкой геймплея, отображением результата игры
    /// </summary>
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