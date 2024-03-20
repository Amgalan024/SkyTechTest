using System;
using UnityEngine;

namespace Utils.BackButtonClickDetector
{
    public class BackButtonClickDetector : MonoBehaviour
    {
        public static BackButtonClickDetector Instance;

        public event Action OnBackButtonClicked;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnBackButtonClicked?.Invoke();
            }
        }
    }
}