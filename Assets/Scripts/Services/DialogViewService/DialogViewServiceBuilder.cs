using System.Collections.Generic;
using Services.DialogView.Views;
using UnityEngine;
using VContainer;

namespace Services.DialogView
{
    public class DialogViewServiceBuilder : BaseInstantServiceBuilder
    {
        [SerializeField] private List<BaseDialogView> _dialogViewPrefabs;
        [SerializeField] private Transform _instantiateParent;
        private DialogViewService _dialogViewService;

        public override object BuildService()
        {
            var dialogViewProvider = new DialogViewProvider(_dialogViewPrefabs, _instantiateParent);
            _dialogViewService = new DialogViewService(dialogViewProvider);
            return _dialogViewService;
        }

        public override void RegisterService(IContainerBuilder builder)
        {
            builder.RegisterInstance(_dialogViewService);
        }
    }
}