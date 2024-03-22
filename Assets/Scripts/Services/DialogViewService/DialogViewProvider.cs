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
        private readonly Transform _instantiateParent;
        private readonly List<BaseDialogView> _dialogViewPool = new();

        public DialogViewProvider(List<BaseDialogView> dialogViewPrefabs, Transform instantiateParent)
        {
            _dialogViewPrefabs = dialogViewPrefabs;
            _instantiateParent = instantiateParent;
        }

        public TDialogView GetDialogView<TDialogView>() where TDialogView : BaseDialogView
        {
            var prefab = _dialogViewPrefabs.FirstOrDefault(d => d.GetType() == typeof(TDialogView));
            Assert.IsNotNull(prefab);

            var pooledDialog = _dialogViewPool.FirstOrDefault(d => d.Available && d.GetType() == typeof(TDialogView));
            if (pooledDialog != null)
            {
                return pooledDialog as TDialogView;
            }
            else
            {
                var dialogView = Object.Instantiate(prefab, _instantiateParent) as TDialogView;
                _dialogViewPool.Add(dialogView);
                return dialogView;
            }
        }
    }
}