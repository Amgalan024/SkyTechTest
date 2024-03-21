using Core.Gameplay.InputStrategies;

namespace Core.Gameplay.Models
{
    public class GameplaySettings
    {
        public PlayerInputStrategyModel PlayerInputStrategyModel { get; }
        public IInputStrategyModel OpponentStrategyInputModel { get; }
        public int TotalRounds { get; }
        public int FieldSize { get; }
        public int LineWinLenght { get; }
        public int ScoreReward { get; }

        public GameplaySettings(PlayerInputStrategyModel playerInputStrategyModel,
            IInputStrategyModel opponentStrategyInputModel, int totalRounds, int fieldSize,
            int lineWinLenght, int scoreReward)
        {
            PlayerInputStrategyModel = playerInputStrategyModel;
            OpponentStrategyInputModel = opponentStrategyInputModel;
            TotalRounds = totalRounds;
            FieldSize = fieldSize;
            LineWinLenght = lineWinLenght;
            ScoreReward = scoreReward;
        }
    }
}