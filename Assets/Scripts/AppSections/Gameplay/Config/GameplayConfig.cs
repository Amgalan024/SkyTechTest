using AppSections.Gameplay.Views;
using UnityEngine;

namespace AppSections.Gameplay.Config
{
    [CreateAssetMenu(fileName = nameof(GameplayConfig), menuName = "Configs/" + nameof(GameplayConfig))]
    public class GameplayConfig : ScriptableObject
    {
        [field: SerializeField] public FieldView FieldPrefab { get; private set; }
        [field: SerializeField] public FieldCellView FieldCellPrefab { get; private set; }
    }
}