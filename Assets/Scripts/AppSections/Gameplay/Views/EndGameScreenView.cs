using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace AppSections.Gameplay.Views
{
    public class EndGameScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _scoreChangeText;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public async UniTask AddWinScore(int oldScore, int newScore, int addedScore)
        {
            gameObject.SetActive(true);

            _scoreText.text = $"Your score: {oldScore}";
            _scoreChangeText.text = $"+{addedScore}";
            var color = Color.green;
            color.a = 0;
            _scoreChangeText.color = color;

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            _scoreText.text = $"Your score: {newScore}";

            color.a = 1;
            await _scoreChangeText.DOColor(color, 0.5f).AsyncWaitForCompletion();
            color.a = 0;
            await _scoreChangeText.DOColor(color, 0.5f).AsyncWaitForCompletion();

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        }

        public async UniTask AddLoseScore(int oldScore, int newScore, int addedScore)
        {
            gameObject.SetActive(true);

            _scoreText.text = $"Your score: {oldScore}";
            _scoreChangeText.text = $"-{addedScore}";
            var color = Color.red;
            color.a = 0;
            _scoreChangeText.color = color;

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            _scoreText.text = $"Your score: {newScore}";

            color.a = 1;
            await _scoreChangeText.DOColor(color, 0.5f).AsyncWaitForCompletion();
            color.a = 0;
            await _scoreChangeText.DOColor(color, 0.5f).AsyncWaitForCompletion();

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        }
    }
}