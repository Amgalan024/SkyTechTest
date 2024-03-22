using System;

namespace AppSections.MainMenu.Models
{
    [Serializable]
    public class GameplaySetupSettingsData
    {
        public string FieldSizeSetupName;
        public string TotalRoundsSetupName;
        public string LineWinLeghtSetupName;

        public int MinFieldSize;
        public int MaxFieldSize;

        public int MinRounds;
        public int MaxRounds;
        public int ScoreRewards;
    }
}