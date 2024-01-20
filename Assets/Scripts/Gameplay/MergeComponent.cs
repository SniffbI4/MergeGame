using System;
using DG.Tweening;
using UnityEngine;

namespace Scripts.GamePlay
{
    public class MergeComponent : MonoBehaviour
    {
        public static event Action<MergeElement, MergeElement> OnCollision;

        public MergeElement Element => _element;
        [SerializeField] private MergeElement _element;


        private void Start()
        {
            Vector3 startScale = transform.localScale;
            transform.localScale = Vector3.zero;
            transform.DOScale(startScale, 0.2f)
                     .SetLink(gameObject)
                     .SetEase(Ease.OutBack);    
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out MergeElement colElement))
            {
                if (_element.Level == colElement.Level)
                {
                    if (Element.ID < colElement.ID)
                        return;
                    
                    OnCollision?.Invoke(Element, colElement);
                }
            }
        }
    }
}