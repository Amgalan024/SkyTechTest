using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AppSections.MainMenu.Views.DialogView
{
    public class IntValueSetupSlider : MonoBehaviour
    {
        public event Action<int> OnValueChanged;

        [SerializeField] private TextMeshProUGUI _selectedValueText;
        [SerializeField] private TextMeshProUGUI _minValueText;
        [SerializeField] private TextMeshProUGUI _maxValueText;
        [SerializeField] private Slider _valueSlider;

        public int Value => (int) _valueSlider.value;
        public int MinValue => (int) _valueSlider.minValue;
        public int MaxValue => (int) _valueSlider.maxValue;

        public void Setup(string valueName, int minValue, int maxValue, int startValue)
        {
            _valueSlider.wholeNumbers = true;
   
            SetValues(minValue,maxValue,startValue);

            _valueSlider.onValueChanged.AddListener(value =>
            {
                OnValueChanged?.Invoke((int) value);
                _selectedValueText.text = $"{valueName} : {(int) value}";
            });

            _selectedValueText.text = $"{valueName} : {startValue}";
        }

        public void SetValues(int minValue, int maxValue, int currentValue)
        {
            _valueSlider.minValue = minValue;
            _valueSlider.maxValue = maxValue;
            _valueSlider.value = currentValue;
            
            _minValueText.text = minValue.ToString();
            _maxValueText.text = maxValue.ToString();
        }
    }
}