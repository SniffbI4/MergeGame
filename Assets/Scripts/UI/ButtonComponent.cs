using System;
using DG.Tweening;
using Scripts.Audio;
using Scripts.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public abstract class ButtonComponent : MonoBehaviour
    {
        public event Action OnButtonPressed;
        public event Action<bool> OnButtonPressedWithState;
        
        [SerializeField] protected Button _button;

        [Space(20)]
        [SerializeField] private float _scaleMultipliyer = 1.1f;
        [SerializeField] private float _scaleTime = 0.1f;

        private AudioManager _audioManager;
        private Sequence _scaleTween;
        

        private void Start()
        {
            _audioManager = GameManager.Instance.GetService<AudioManager>();
            
            _button.onClick.AddListener(HandleClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        protected void ShowButtonEffect()
        {
            _scaleTween?.Kill();
            
            _scaleTween = DOTween.Sequence();
                    
            _scaleTween.Append(transform.DOScale(_scaleMultipliyer, _scaleTime))
                       .Append(transform.DOScale(Vector3.one, _scaleTime))
                       .SetLink(gameObject);
        }

        protected virtual void HandleClick()
        {
            _audioManager.PlayClipByType(ClipType.Click);
            ShowButtonEffect();
        }

        protected void PressButton() => 
                OnButtonPressed?.Invoke();

        protected void PressButtonWithState(bool state) => 
                OnButtonPressedWithState?.Invoke(state);
    }
}