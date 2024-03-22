namespace AppSections.Gameplay.InputStrategies
{
    public class PlayerInputStrategyModel : IInputStrategyModel
    {
        public string Name { get; }
        public string Id { get; }

        public PlayerInputStrategyModel(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }
}