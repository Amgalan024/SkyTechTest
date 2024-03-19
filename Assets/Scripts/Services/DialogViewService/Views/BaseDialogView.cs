using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Utils.DialogView.Views
{
    public abstract class BaseDialogView : MonoBehaviour
    {
        public event Action OnCloseClicked;

        [SerializeField] private Button _closeButton;

        public abstract void Setup(object setupData);
        public abstract UniTask ShowAsync();
        public abstract UniTask HideAsync();

        public void BaseSetup()
        {
            _closeButton.onClick.AddListener(() =>
            {
                _closeButton.onClick.RemoveAllListeners();
                OnCloseClicked?.Invoke();
                HideAsync();
            });
        }
    }
}