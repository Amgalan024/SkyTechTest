namespace Core.Gameplay.Models
{
    public class GameplayResult
    {
        public string WinnerName { get; }
        public int GameDuration { get; }

        public GameplayResult(string winnerName, int gameDuration)
        {
            WinnerName = winnerName;
            GameDuration = gameDuration;
        }
    }
}