using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AppSections.Gameplay.Views
{
    public class GameplayView : MonoBehaviour
    {
        public event Action OnPauseClicked;

        [field: SerializeField] public PauseView PauseView { get; private set; }
        [field: SerializeField] public EndGameScreenView EndGameScreenView { get; private set; }
        [SerializeField] private TextMeshProUGUI _playerNameText;
        [SerializeField] private TextMeshProUGUI _opponentNameText;
        [SerializeField] private TextMeshProUGUI _playerRoundsText;
        [SerializeField] private TextMeshProUGUI _opponentRoundsText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _turnText;
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

        public void SetPlayerRoundsText(int currentRound, int totalRounds)
        {
            _playerRoundsText.text = $"{currentRound}/{totalRounds}";
        }

        public void SetOpponentRoundsText(int currentRound, int totalRounds)
        {
            _opponentRoundsText.text = $"{currentRound}/{totalRounds}";
        }

        public void SetTurnName(string turnName)
        {
            _turnText.text = $"Turn: {turnName}";
        }

        public void SetTime(DateTime time)
        {
            _timerText.text = time.ToString("mm:ss");
        }
    }
}