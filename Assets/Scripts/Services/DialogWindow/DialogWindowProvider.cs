using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Utils.DialogWindow.Views;

namespace Utils.DialogWindow
{
    public class DialogWindowProvider
    {
        private readonly List<BaseDialogWindow> _dialogWindowPrefabs;

        public DialogWindowProvider(List<BaseDialogWindow> dialogWindowPrefabs)
        {
            _dialogWindowPrefabs = dialogWindowPrefabs;
        }

        public TDialogWindow GetDialogWindow<TDialogWindow>() where TDialogWindow : BaseDialogWindow
        {
            var prefab = _dialogWindowPrefabs.FirstOrDefault(d => d.GetType() == typeof(TDialogWindow));
            Assert.IsNotNull(prefab);

            var dialogWindow = Object.Instantiate(prefab) as TDialogWindow;

            return dialogWindow;
        }
    }
}