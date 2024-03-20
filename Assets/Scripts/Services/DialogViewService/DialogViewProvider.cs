using System.Collections.Generic;
using System.Linq;
using Services.DialogView.Views;
using UnityEngine;
using UnityEngine.Assertions;

namespace Services.DialogView
{
    public class DialogViewProvider
    {
        private readonly List<BaseDialogView> _dialogViewPrefabs;
        private Transform _instantiateParent;

        public DialogViewProvider(List<BaseDialogView> dialogViewPrefabs, Transform instantiateParent)
        {
            _dialogViewPrefabs = dialogViewPrefabs;
            _instantiateParent = instantiateParent;
        }

        //todo: сделать пул вьюшек
        public TDialogView GetDialogView<TDialogView>() where TDialogView : BaseDialogView
        {
            var prefab = _dialogViewPrefabs.FirstOrDefault(d => d.GetType() == typeof(TDialogView));
            Assert.IsNotNull(prefab);

            var dialogView = Object.Instantiate(prefab, _instantiateParent) as TDialogView;

            return dialogView;
        }
    }
}