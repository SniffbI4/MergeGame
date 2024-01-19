using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game
{
    public class GameFlowInstaller : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _gameObjects;

        private GameFlow _gameFlow;
        
        public void InstallGameListeners(GameFlow gameFlow)
        {
            _gameFlow = gameFlow;

            foreach (GameObject obj in _gameObjects)
            {
                IGameListener[] listeners = obj.GetComponentsInChildren<IGameListener>();

                foreach (IGameListener listener in listeners)
                {
                    _gameFlow.AddListener(listener);
                }
            }
        }
    }
}