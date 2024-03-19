namespace Core.Gameplay.Models
{
    public class GameplaySettings
    {
        public string Name { get; }

        public GameplaySettings(string name)
        {
            Name = name;
        }
    }
}