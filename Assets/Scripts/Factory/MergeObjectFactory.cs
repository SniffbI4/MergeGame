using System.Collections.Generic;
using Scripts.Game;
using Scripts.Progress;
using Scripts.GamePlay;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Factory
{
    public class MergeObjectFactory : MonoBehaviour,
                                      IGameInitListener
    {
        private GameProgressController _progressController;

        void IGameInitListener.OnGameInit()
        {
            _progressController = GameManager.Instance.GetService<GameProgressController>();
        }

        public MergeElement CreateRandObject()
        {
            List<ProgressData> availableObjects = _progressController.GetAvailableObjects();
            ProgressData randObject = availableObjects[Random.Range(0, availableObjects.Count)];
            return CreateObjectByPrefab(randObject);
        }

        public MergeElement CreateNextGradeObject(MergeElement current)
        {
            return CreateObjectByLevel(current.Level + 1);
        }

        private MergeElement CreateObjectByLevel(int level)
        {
            ProgressData prefabData = _progressController.GetObjectByLevel(level);
            return CreateObjectByPrefab(prefabData);
        }

        private MergeElement CreateObjectByPrefab(ProgressData prefabData)
        {
            if (prefabData != null)
                return Instantiate(prefabData.MainComponent);

            return null;
        }
    }
}