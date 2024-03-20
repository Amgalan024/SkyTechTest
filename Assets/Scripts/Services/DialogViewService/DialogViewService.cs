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

        //todo: сделать пулы к каждому типа вьюшек, то есть открываем диалог, если пул пустой или в нем нет доступных для пула(закрытых) диалогов
        //то создаем и добавляем диалог в список, после закрытия ставится статус что можно пулить, при след запросе берется закрытый диалог из пул-списка
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