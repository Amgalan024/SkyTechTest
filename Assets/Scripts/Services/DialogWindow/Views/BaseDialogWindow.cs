using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Utils.DialogWindow.Views
{
    public abstract class BaseDialogWindow : MonoBehaviour
    {
        public abstract void Setup(object setupData, Action<object> onHide);
        public abstract UniTask ShowAsync();
        public abstract UniTask HideAsync();
    }
}