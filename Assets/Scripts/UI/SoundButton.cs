using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class SoundButton : ButtonComponent
    {
        [SerializeField] private Image _sourceImage;
        [SerializeField] private Sprite _activeIcon;
        [SerializeField] private Sprite _inActiveIcon;
        
        private bool _isMutedState = false;

        protected override void HandleClick()
        {
            base.HandleClick();
            
            _isMutedState = !_isMutedState;
            
            PressButtonWithState(_isMutedState);
            
            ChangeSprite();
        }

        private void ChangeSprite()
        {
            Sprite newSprite = _isMutedState ? _inActiveIcon : _activeIcon;
            _sourceImage.sprite = newSprite;
        }
    }
}