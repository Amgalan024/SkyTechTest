using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Utils.DialogView.Views;

namespace Utils.DialogView
{
    public class DialogViewProvider
    {
        private readonly List<BaseDialogView> _dialogViewPrefabs;

        public DialogViewProvider(List<BaseDialogView> dialogViewPrefabs)
        {
            _dialogViewPrefabs = dialogViewPrefabs;
        }

        public TDialogView GetDialogView<TDialogView>() where TDialogView : BaseDialogView
        {
            var prefab = _dialogViewPrefabs.FirstOrDefault(d => d.GetType() == typeof(TDialogView));
            Assert.IsNotNull(prefab);

            var dialogView = Object.Instantiate(prefab) as TDialogView;

            return dialogView;
        }
    }
}