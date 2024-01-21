using Scripts.Game;
using UnityEngine;

namespace Scripts.GamePlay
{
    public class FinishGameController : MonoBehaviour,
                                        IGameInitListener
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private LayerMask _mask;

        private Collider2D[] results = new Collider2D[1];
        private GameFlow _gameFlow;

        public void OnGameInit()
        {
            _gameFlow = GameManager.Instance.GetService<GameFlow>();
        }

        public void CheckCollision()
        {
            Bounds bounds = new Bounds(_renderer.bounds.center, _collider.bounds.size);
            
            Physics2D.OverlapBoxNonAlloc(bounds.center, bounds.size, 0f, results, _mask);
            
            if (results[0] != null)
                _gameFlow.FinishGame();
        }
    }
}