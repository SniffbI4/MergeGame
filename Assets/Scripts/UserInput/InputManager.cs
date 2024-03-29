﻿using System;
using Scripts.Game;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UserInput
{
    public class InputManager : MonoBehaviour,
                                IGameStartListener,
                                IUpdateListener,
                                IGamePauseListener,
                                IGameResumeListener,
                                IGameFinishListener
    {
        public event Action OnMouseButtonDown;
        public event Action OnMouseButtonUp;
        
        public bool Enable => _enabled;
        public Vector2 MousePosition { get; private set; }

        private bool _enabled;

        void IGameStartListener.OnGameStart() => SetEnable(true);

        void IGamePauseListener.OnGamePaused() => SetEnable(false);

        void IGameResumeListener.OnGameResumed() => SetEnable(true);

        void IGameFinishListener.OnGameFinished() => SetEnable(false);

        public void SetEnable(bool state) => _enabled = state;
        
        void IUpdateListener.Update(float deltaTime)
        {
            if (!_enabled)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    OnMouseButtonDown?.Invoke();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnMouseButtonUp?.Invoke();
            }
            
            MousePosition = Input.mousePosition;
        }
    }
}