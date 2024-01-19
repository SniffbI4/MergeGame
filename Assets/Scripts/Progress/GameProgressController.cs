using System;
using System.Collections.Generic;
using Scripts.Game;
using Scripts.SaveLoad;
using Random = UnityEngine.Random;

namespace Scripts.Progress
{
    public class GameProgressController
    {
        public event Action<int> OnLevelUp;
        
        public GameProgress Config => _progressConfig;
        private GameProgress _progressConfig;

        private SaveLoadManager _saveLoadManager;

        public GameProgressController(GameProgress config)
        {
            _saveLoadManager = GameManager.Instance.GetService<SaveLoadManager>();
            _progressConfig = config;
            SetLevelsToObjects();
            CheckSavedLevel();
        }

        private void SetLevelsToObjects()
        {
            foreach (var data in _progressConfig.GameProgressList)
            {
                data.MainComponent.Level = data.Level;
            }
        }

        public List<ProgressData> GetAvailableObjects()
        {
            float randValue = Random.value;
            return _progressConfig.GameProgressList.FindAll(x => x.IsOpened && x.ChanceToChoose >= randValue);
        }

        public ProgressData GetObjectByLevel(int level)
        {
            var data = _progressConfig.GameProgressList.Find(x => x.Level == level);

            if (data == null)
                return null;
            
            if (!data.IsOpened)
            {
                data.IsOpened = true;
                _saveLoadManager.SaveMaxLevel(level);
                OnLevelUp?.Invoke(level);
            }
            
            return data;
        }

        public int GetScoreByLevel(int level)
        {
            var data = _progressConfig.GameProgressList.Find(x => x.Level == level);
            return data.ScoreWhenMerge;
        }

        private void CheckSavedLevel()
        {
            int currentLevel = _saveLoadManager.LoadProgressLevel();
            if (currentLevel < 1)
            {
                currentLevel = 1;
                _saveLoadManager.SaveMaxLevel(currentLevel);
            }
            foreach (ProgressData data in _progressConfig.GameProgressList)
            {
                data.IsOpened = (data.Level <= currentLevel);
            }
        }
    }
}