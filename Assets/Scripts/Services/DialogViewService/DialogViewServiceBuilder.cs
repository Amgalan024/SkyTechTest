using System.Collections.Generic;
using UnityEngine;
using Utils.DialogView.Views;
using VContainer;

namespace Utils.DialogView
{
    public class DialogViewServiceBuilder : BaseServiceBuilder
    {
        [SerializeField] private List<BaseDialogView> _dialogViewPrefabs;
        [SerializeField] private Transform _instantiateParent;
        private DialogViewService _dialogViewService;

        public override object Build()
        {
            var dialogViewProvider = new DialogViewProvider(_dialogViewPrefabs, _instantiateParent);
            _dialogViewService = new DialogViewService(dialogViewProvider);
            return _dialogViewService;
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_dialogViewService);
        }
    }
}