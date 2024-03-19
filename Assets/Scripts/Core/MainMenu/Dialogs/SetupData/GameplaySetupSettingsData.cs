using System;

namespace Core.MainMenu.Models
{
    [Serializable]
    public class GameplaySetupSettingsData
    {
        public int MinFieldSize;
        public int MaxFieldSize;

        public int MinRounds;
        public int MaxRounds;
    }
}