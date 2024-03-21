﻿using System.Collections.Generic;
using Core.Gameplay.Models;
using Core.Gameplay.Views;
using UnityEngine;

namespace Core.Gameplay
{
    public class FieldConstructor
    {
        public Dictionary<FieldCellView, FieldCellModel> FieldCellModelsByView { get; } = new();
        public Dictionary<FieldCellModel, FieldCellView> FieldCellViewsByModel { get; } = new();
        public List<FieldCellModel> FieldCellModels { get; } = new();
        public List<FieldCellView> FieldCellViews { get; } = new();

        private readonly FieldView _fieldView;
        private readonly FieldCellView _fieldCellViewPrefab;

        public FieldConstructor(FieldCellView fieldCellViewPrefab, FieldView fieldView)
        {
            _fieldCellViewPrefab = fieldCellViewPrefab;
            _fieldView = fieldView;
        }

        /// <summary>
        /// Сетка с координатами вида
        ///   0 1 2 3 4 → X координаты
        /// 0 x x x x x
        /// 1 x x x x x
        /// 2 x x x x x
        /// 3 x x x x x
        /// 4 x x x x x
        /// ↓
        /// Y координаты
        /// </summary>
        /// <param name="size"></param>
        public void CreateField(int size)
        {
            var cellSize = _fieldView.Size / (float) size;
            var cellOffset = _fieldView.Size / (float) (size);

            var centerPosition = _fieldView.CenterPoint.position;
            var startPositionOffset = 0f;
            if (size % 2 == 1)
            {
                startPositionOffset = (cellOffset * (int) (size / 2));
            }
            else
            {
                startPositionOffset = (cellOffset * (int) (size / 2)) - (cellOffset / 2);
            }

            var startPositionX = centerPosition.x - startPositionOffset;
            var startPositionY = centerPosition.y + startPositionOffset;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    var positionX = startPositionX + (cellOffset * x);
                    var positionY = startPositionY - (cellOffset * y);

                    var position = new Vector2(positionX, positionY);

                    var fieldCellView = Object.Instantiate(_fieldCellViewPrefab, position, Quaternion.identity,
                        _fieldView.CellsContainer);

                    fieldCellView.SetSize(cellSize);
                    fieldCellView.SetClaimed(string.Empty);
                    var gridPosition = new Vector2(x, y);
                    var fieldCellModel = new FieldCellModel(gridPosition);

                    FieldCellModelsByView.Add(fieldCellView, fieldCellModel);
                    FieldCellViewsByModel.Add(fieldCellModel, fieldCellView);
                    FieldCellModels.Add(fieldCellModel);
                    FieldCellViews.Add(fieldCellView);
                }
            }
        }
    }
}