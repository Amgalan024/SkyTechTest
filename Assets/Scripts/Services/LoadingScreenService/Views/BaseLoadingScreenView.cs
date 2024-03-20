using UnityEngine;

namespace Services.LoadingScreen
{
    public abstract class BaseLoadingScreenView : MonoBehaviour
    {
        public abstract void Setup(object setupData);
        public abstract void Show();
        public abstract void Hide();
        public abstract void SetLoadingProgress(float value);
        public abstract void SetLoadingText(string text);
    }
}