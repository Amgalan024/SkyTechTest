using Core.Gameplay.InputStrategies;

namespace Core.Gameplay.Models
{
    public class GameplaySettings
    {
        public PlayerInputStrategyModel PlayerInputStrategyModel { get; }
        public BotStrategyInputModel BotStrategyInputModel { get; }
        public int TotalRounds { get; }
        public int FieldSize { get; }
        public int LineWinLenght { get; }
        public int ScoreReward { get; }

        public GameplaySettings(PlayerInputStrategyModel playerInputStrategyModel,
            BotStrategyInputModel botStrategyInputModel, int totalRounds, int fieldSize,
            int lineWinLenght, int scoreReward)
        {
            PlayerInputStrategyModel = playerInputStrategyModel;
            BotStrategyInputModel = botStrategyInputModel;
            TotalRounds = totalRounds;
            FieldSize = fieldSize;
            LineWinLenght = lineWinLenght;
            ScoreReward = scoreReward;
        }
    }
}