using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Game
{
    [Serializable]
    public class GameFlow
    {
        [ShowInInspector]
        private HashSet<IGameListener> _listeners = new();
        [ShowInInspector]
        private HashSet<IUpdateListener> _updateListeners = new();
        [ShowInInspector]
        private HashSet<IFixedUpdateListener> _fixedUpdateListeners = new();

        private bool _onPause = false;

        public void AddListener(IGameListener listener)
        {
            _listeners.Add(listener);
            
            if (listener is IUpdateListener updateListener)
            {
                _updateListeners.Add(updateListener);
            }

            if (listener is IFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Add(fixedUpdateListener);
            }
        }
        
        public void InitGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameInitListener initListener)
                    initListener.OnGameInit();
            }

            Debug.Log("GAME INITED");
        }

        [Button]
        public void StartGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameStartListener startListener)
                    startListener.OnGameStart();
            }
            
            Debug.Log("GAME STARTED");
        }

        public void Update(float deltaTime)
        {
            if (_onPause)
                return;
            
            foreach (IUpdateListener listener in _updateListeners)
            {
                listener.Update(deltaTime);
            }
        }

        public void FixedUpdate(float deltaTime)
        {
            if (_onPause)
                return;

            foreach (IFixedUpdateListener listener in _fixedUpdateListeners)
            {
                listener.FixedUpdate(deltaTime);
            }
        }

        [Button]
        public void FinishGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameFinishListener finishListener)
                    finishListener.OnGameFinished();
            }

            _onPause = true;
            //Time.timeScale = 0f;
            Debug.Log("GAME FINISHED");
        }

        [Button]
        public void PauseGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGamePauseListener pauseListener)
                    pauseListener.OnGamePaused();
            }

            _onPause = true;
            //Time.timeScale = 0f;
            Debug.Log("GAME PAUSED");
        }

        [Button]
        public void ResumeGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameResumeListener resumeListener)
                    resumeListener.OnGameResumed();
            }

            _onPause = false;
            //Time.timeScale = 1f;
            Debug.Log("GAME RESUMED");
        }

        [Button]
        public void RestartGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameRestartListener restartListener)
                    restartListener.OnGameRestarted();
            }

            _onPause = false;
            //Time.timeScale = 1f;
            Debug.Log("GAME RESTARTED");
            
            StartGame();
        }
    }
}