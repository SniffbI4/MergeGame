using System;
using Scripts.Pool;
using TMPro;
using UnityEngine;

namespace FloatingText
{
    public class FloatingText : MonoBehaviour,
                                IPoolObject
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private TMP_Text _damageText;

        private bool _follow = false;
        private Vector3 _creationPoint;

        public event Action<IPoolObject> OnObjectNeededToDeactivate;

        public void SetText(string text)
        {
            _damageText.text = text;
            AnimatorClipInfo[] clipInfo = _animator.GetCurrentAnimatorClipInfo(0);
            Invoke(nameof(TurnOff), clipInfo[0].clip.length);
        }

        public void SetPoint(Vector3 locationPosition, bool needToFollow)
        {
            _follow = needToFollow;
            _creationPoint = locationPosition;
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(_creationPoint);
            transform.position = screenPosition;
        }

        public void SetColor(Color color)
        {
            _damageText.color = color;
        }

        private void Update()
        {
            if (_follow)
            {
                Vector2 screenPosition = Camera.main.WorldToScreenPoint(_creationPoint);
                transform.position = screenPosition;
            }
        }

        public void ResetBeforeBackToPool()
        {
            gameObject.SetActive(false);
            transform.localScale = Vector3.one;
        }

        private void TurnOff()
        {
            OnObjectNeededToDeactivate?.Invoke(this);
        }
    }
}