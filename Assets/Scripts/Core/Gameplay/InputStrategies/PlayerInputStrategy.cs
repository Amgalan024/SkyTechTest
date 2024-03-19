﻿using System;
using System.Collections.Generic;
using Core.Gameplay.Models;
using Core.Gameplay.Views;

namespace Core.Gameplay.InputStrategies
{
    public class PlayerInputStrategy : IInputStrategy
    {
        public event Action<FieldCellModel> OnInput;

        private readonly Dictionary<FieldCellView, FieldCellModel> _fieldCellModelsByView;

        private readonly string _id;
        private bool _canInput;

        public PlayerInputStrategy(string id, Dictionary<FieldCellView, FieldCellModel> fieldCellModelsByView)
        {
            _fieldCellModelsByView = fieldCellModelsByView;
            _id = id;
        }

        void IInputStrategy.HandleInput()
        {
            _canInput = true;
        }

        public void Initialize()
        {
            foreach (var fieldCellView in _fieldCellModelsByView.Keys)
            {
                fieldCellView.OnClicked += OnFieldCellViewClicked;
            }
        }

        private void OnFieldCellViewClicked(FieldCellView fieldCellView)
        {
            var fieldCellModel = _fieldCellModelsByView[fieldCellView];

            if (_canInput)
            {
                fieldCellModel.ClaimCell(_id);
                OnInput?.Invoke(fieldCellModel);
            }

            _canInput = false;
        }
    }
}