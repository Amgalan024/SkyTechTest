using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.MainMenu.Views.DialogView
{
    public class IntValueSetupSlider : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _selectedValueText;
        [SerializeField] private TextMeshProUGUI _minValueText;
        [SerializeField] private TextMeshProUGUI _maxValueText;
        [SerializeField] private Slider _valueSlider;

        public int Value => (int) _valueSlider.value;

        public void Setup(int minValue, int maxValue)
        {
            _valueSlider.wholeNumbers = true;
            _valueSlider.minValue = minValue;
            _valueSlider.maxValue = maxValue;
            _valueSlider.value = minValue;

            _minValueText.text = minValue.ToString();
            _maxValueText.text = maxValue.ToString();
            _selectedValueText.text = minValue.ToString();

            _valueSlider.onValueChanged.AddListener(value => _selectedValueText.text = ((int) value).ToString());
        }
    }
}