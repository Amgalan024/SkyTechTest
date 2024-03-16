using System.Collections.Generic;
using UnityEngine;
using Utils.DialogView.Views;
using VContainer;

namespace Utils.DialogView
{
    public class DialogViewServiceBuilder : BaseServiceBuilder
    {
        [SerializeField] private List<BaseDialogView> _dialogViewPrefabs;

        public override void Build(IContainerBuilder builder)
        {
            builder.RegisterInstance(_dialogViewPrefabs);
            builder.Register<DialogViewProvider>(Lifetime.Singleton).AsSelf();
            builder.Register<DialogViewService>(Lifetime.Singleton).AsSelf();
        }
    }
}