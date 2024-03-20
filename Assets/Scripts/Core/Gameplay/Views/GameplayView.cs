using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Gameplay.Views
{
    public class GameplayView : MonoBehaviour
    {
        public event Action OnPauseClicked;

        [field: SerializeField] public PauseView PauseView { get; private set; }
        [field: SerializeField] public EndGameScreenView EndGameScreenView { get; private set; }
        [SerializeField] private TextMeshProUGUI _playerNameText;
        [SerializeField] private TextMeshProUGUI _opponentNameText;
        [SerializeField] private TextMeshProUGUI _roundsCounterText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Button _pauseButton;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(() => { OnPauseClicked?.Invoke(); });
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveAllListeners();
        }

        public void SetPlayerName(string gameName)
        {
            _playerNameText.text = gameName;
        }

        public void SetOpponentName(string gameName)
        {
            _opponentNameText.text = gameName;
        }

        public void SetRoundsCounterStatus(int currentRound, int totalRounds)
        {
            _roundsCounterText.text = $"{currentRound}/{totalRounds}";
        }

        public void SetTime(int seconds)
        {
            var dateTime = new DateTime().AddSeconds(seconds);//todo: Оптимизировать, передавать DateTime в меетод, хранить его в таймере мб или в контроллере

            _timerText.text = dateTime.ToString("mm:ss ");
        }
    }
}