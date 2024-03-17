﻿using System.Collections.Generic;
using UnityEngine;
using Utils.DialogView.Views;
using VContainer;

namespace Utils.DialogView
{
    public class DialogViewServiceBuilder : BaseServiceBuilder
    {
        [SerializeField] private List<BaseDialogView> _dialogViewPrefabs;
        private DialogViewService _dialogViewService;

        public override IService Build()
        {
            var dialogViewProvider = new DialogViewProvider(_dialogViewPrefabs);
            _dialogViewService = new DialogViewService(dialogViewProvider);
            return _dialogViewService;
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_dialogViewService);
        }
    }
}