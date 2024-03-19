namespace Core.Gameplay.Models
{
    public class GameplayResult
    {
        public string WinnerName { get; }

        public GameplayResult(string winnerName)
        {
            WinnerName = winnerName;
        }
    }
}