using Scripts.Game;
using Scripts.UserInput;
using UnityEngine;

namespace Scripts.GamePlay
{
    public class SpawnController : MonoBehaviour,
                                   IGameInitListener
    {
        private InputManager _inputManager;
        private ObjectSpawner _spawner;
        private Camera _camera;

        private bool _inMove = false;
                
        public void OnGameInit()
        {
            _inputManager = GameManager.Instance.GetService<InputManager>();
            _spawner = GameManager.Instance.GetService<ObjectSpawner>();
            _camera = Camera.main;

            _inputManager.OnMouseButtonDown += StartMove;
            _inputManager.OnMouseButtonUp += SpawnObject;
        }

        private void SpawnObject()
        {
            if (_inMove == false)
                return;

            _inMove = false;
            _spawner.Spawn();
        }

        private void OnDestroy()
        {
            _inputManager.OnMouseButtonDown -= StartMove;
            _inputManager.OnMouseButtonUp -= SpawnObject;
        }

        private void StartMove()
        {
            _inMove = true;
        }

        private void Update()
        {
            if (!_inMove)
                return;

            var mousePos = _inputManager.MousePosition;
            Vector2 direction = _camera.ScreenToWorldPoint(mousePos);
            _spawner.MoveSpawnPoint(direction);
        }
    }
}