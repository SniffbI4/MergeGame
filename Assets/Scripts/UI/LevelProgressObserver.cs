using System.Collections.Generic;
using Scripts.Game;
using Scripts.Progress;
using UnityEngine;

namespace Scripts.UI
{
    public class LevelProgressObserver : MonoBehaviour
    {
        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private ObjectUISlot _slotPrefab;

        private GameProgressController _progressController;

        private Dictionary<int, ObjectUISlot> _slotsMap = new();

        private void Awake()
        {
            _progressController = GameManager.Instance.GetService<GameProgressController>();
            _progressController.OnLevelUp += OnLevelUp;
            
            foreach (ProgressData data in _progressController.Config.GameProgressList)
            {
                Sprite objSprite = data.MainComponent.SpriteRendererComponent.sprite;
                var slot = Instantiate(_slotPrefab, _slotsContainer);
                _slotsMap[data.Level] = slot;
                slot.SetIcons(objSprite, data.IsOpened);
            }
        }

        private void OnDestroy()
        {
            _progressController.OnLevelUp -= OnLevelUp;
        }

        private void OnLevelUp(int level)
        {
            _slotsMap[level].OpenSlot();
        }
    }
}