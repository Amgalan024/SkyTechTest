namespace Core.Gameplay.Models
{
    public class GameplaySettings
    {
        public string PlayerName { get; }
        public string OpponentName { get; }
        public int TotalRounds { get; }
        public int FieldSize { get; }
        public int LineWinLenght { get; }//todo:ограничить по значению не больше размера поля
        public int ScoreReward { get; }

        public GameplaySettings(string playerName, string opponentName, int totalRounds, int fieldSize, int lineWinLenght, int scoreReward)
        {
            PlayerName = playerName;
            OpponentName = opponentName;
            TotalRounds = totalRounds;
            FieldSize = fieldSize;
            LineWinLenght = lineWinLenght;
            ScoreReward = scoreReward;
        }
    }
}