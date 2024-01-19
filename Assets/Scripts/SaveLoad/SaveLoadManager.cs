using UnityEngine;

namespace Scripts.SaveLoad
{
    public class SaveLoadManager
    {
        private const string ScoreKey = "Record";
        private const string MaxLevel = "MaxLevel";
        
        public void SaveScoreRecord(int score)
        {
            PlayerPrefs.SetInt(ScoreKey, score);
        }

        public int LoadScoreRecord()
        {
            if (PlayerPrefs.HasKey(ScoreKey))
            {
                return PlayerPrefs.GetInt(ScoreKey);
            }
            else
            {
                return 0;
            }
        }

        public void SaveMaxLevel(int maxLevel)
        {
            int currentMaxLevel = PlayerPrefs.GetInt(MaxLevel);
            if (maxLevel > currentMaxLevel)
            {
                PlayerPrefs.SetInt(MaxLevel, maxLevel);
            }
        }

        public int LoadProgressLevel()
        {
            if (PlayerPrefs.HasKey(MaxLevel))
            {
                return PlayerPrefs.GetInt(MaxLevel);
            }
            else
            {
                return 0;
            }
        }
    }
}