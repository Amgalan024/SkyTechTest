using System;

namespace Core.Gameplay.Models
{
    public class GameplayResult
    {
        public string WinnerName { get; }
        public DateTime GameDuration { get; }

        public GameplayResult(string winnerName, DateTime gameDuration)
        {
            WinnerName = winnerName;
            GameDuration = gameDuration;
        }
    }
}