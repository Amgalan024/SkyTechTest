using TMPro;
using UnityEngine;

namespace Core.Gameplay.Views
{
    public class GameplayView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerNameText;
        [SerializeField] private TextMeshProUGUI _opponentNameText;
        [SerializeField] private TextMeshProUGUI _roundsCounterText;

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
    }
}