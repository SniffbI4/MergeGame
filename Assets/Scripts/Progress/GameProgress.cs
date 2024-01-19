using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.GamePlay;
using UnityEngine;

namespace Scripts.Progress
{
    [CreateAssetMenu(menuName = "Configs/Progress", fileName = "ProgressList")]
    public class GameProgress : ScriptableObject
    {
        public List<ProgressData> GameProgressList;

        private void OnValidate()
        {
            if (!GameProgressList.Any(x => x.IsOpened))
                Debug.LogError($"There is no oppened objects in LevelConfig file. Check it");

            if (GameProgressList.GroupBy(x => x.Level).Any(g => g.Count() > 1))
                Debug.LogError($"There are objects with dublicated Level parameters");
        }
    }

    [Serializable]
    public class ProgressData
    {
        public bool IsOpened;
        [Range(0f, 1f)]
        public float ChanceToChoose;
        public MergeElement MainComponent;
        public int Level;
        public int ScoreWhenMerge;
    }
}