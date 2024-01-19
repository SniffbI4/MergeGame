using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class SoundButton : ButtonComponent
    {
        [SerializeField] private Image _sourceImage;
        [SerializeField] private Sprite _activeIcon;
        [SerializeField] private Sprite _inActiveIcon;
        
        private bool _state = true;

        protected override void HandleClick()
        {
            base.HandleClick();
            
            _state = !_state;
            
            PressButtonWithState(_state);
            
            ChangeSprite();
        }

        private void ChangeSprite()
        {
            Sprite newSprite = _state ? _activeIcon : _inActiveIcon;
            _sourceImage.sprite = newSprite;
        }
    }
}