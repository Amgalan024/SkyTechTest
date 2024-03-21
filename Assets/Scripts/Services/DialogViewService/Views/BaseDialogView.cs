using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Services.DialogView.Views
{
    public abstract class BaseDialogView : MonoBehaviour, IPoolable
    {
        public event Action OnClosed;

        [SerializeField] private Button _closeButton;

        public abstract void Setup(object setupData);
        protected abstract UniTask DoOnShowAsync();
        protected abstract UniTask DoOnHideAsync();

        public bool Available { get; private set; }

        public void BaseSetup()
        {
            _closeButton.onClick.AddListener(() =>
            {
                _closeButton.onClick.RemoveAllListeners();
                HideAsync().Forget();
            });
        }

        public async UniTask ShowAsync()
        {
            Available = false;
            await DoOnShowAsync();
        }

        public async UniTask HideAsync()
        {
            await DoOnHideAsync();
            OnClosed?.Invoke();
            Available = true;
        }
    }
}