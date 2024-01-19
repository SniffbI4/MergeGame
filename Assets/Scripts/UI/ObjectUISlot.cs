using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class ObjectUISlot : MonoBehaviour
    {
        [SerializeField] private Image _inactiveImage;
        [SerializeField] private Image _activeImage;
        
        public void SetIcons(Sprite sprite, bool isOppened = false)
        {
            _inactiveImage.sprite = sprite;
            _activeImage.sprite = sprite;
            
            if (isOppened)
                OpenSlot();
        }

        public void OpenSlot()
        {
            _activeImage.gameObject.SetActive(true);
        }
    }
}