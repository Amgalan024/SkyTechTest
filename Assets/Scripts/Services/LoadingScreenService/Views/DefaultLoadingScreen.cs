using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Utils.LoadingScreen.SetupData;

namespace Utils.LoadingScreen
{
    public class DefaultLoadingScreen : BaseLoadingScreenView
    {
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Slider _loadingSlider;
        [SerializeField] private TextMeshProUGUI _loadingText;

        public override void Setup(object setupData)
        {
            var defaultSetupData = (DefaultLoadingScreenSetupData) setupData;

            Assert.IsNotNull(defaultSetupData);

            _headerText.text = defaultSetupData.HeaderText;
            _backgroundImage.color = defaultSetupData.BackgroundColor;
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void SetLoadingProgress(float value)
        {
            _loadingSlider.value = value;
        }

        public override void SetLoadingText(string text)
        {
            _loadingText.text = text;
        }
    }
}