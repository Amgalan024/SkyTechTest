using System.Collections.Generic;
using UnityEngine;
using Utils.DialogWindow.Views;
using VContainer;

namespace Utils.DialogWindow
{
    public class DialogWindowServiceBuilder : BaseServiceBuilder
    {
        [SerializeField] private List<BaseDialogWindow> _dialogWindowPrefabs;

        public override void Build(IContainerBuilder builder)
        {
            builder.RegisterInstance(_dialogWindowPrefabs);
            builder.Register<DialogWindowProvider>(Lifetime.Singleton).AsSelf();
            builder.Register<DialogWindowService>(Lifetime.Singleton).AsSelf();
        }
    }
}