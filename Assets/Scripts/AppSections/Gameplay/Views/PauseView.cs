using System;
using UnityEngine;
using UnityEngine.UI;

namespace AppSections.Gameplay.Views
{
    public class PauseView : MonoBehaviour
    {
        public event Action OnResumeClicked;
        public event Action OnMainMenuClicked;

        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _mainMenuButton;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(() => { OnResumeClicked?.Invoke(); });
            _mainMenuButton.onClick.AddListener(() => { OnMainMenuClicked?.Invoke(); });
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}