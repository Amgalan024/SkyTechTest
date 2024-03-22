namespace AppSections.Gameplay.InputStrategies
{
    public class BotStrategyInputModel : IInputStrategyModel
    {
        public string Id { get; }
        public string Name { get; }
        public int Difficulty { get; }

        public BotStrategyInputModel(string name, int difficulty, string id)
        {
            Name = name;
            Difficulty = difficulty;
            Id = id;
        }

    }
}