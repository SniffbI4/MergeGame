using System.Collections;
using System.Collections.Generic;
using Scripts.Audio;
using Scripts.Factory;
using Scripts.Game;
using Scripts.UserInput;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.GamePlay
{
    public class ObjectSpawner : MonoBehaviour,
                                 IGameInitListener,
                                 IGameStartListener,
                                 IUpdateListener,
                                 IGameFinishListener,
                                 IGameRestartListener
    {

        private float _moveSpeed = 5f;
        
        [SerializeField] private MergeObjectFactory _factory;
        
        
        
        [SerializeField] private Transform _spawnTransform;

        [SerializeField] private Transform _leftSpawnPoint;
        private float _leftBoard;
        [SerializeField] private Transform _rightSpawnPoint;
        private float _rightBoard;
        
        [SerializeField] private GameObject _ray;

        [ShowInInspector]
        private List<MergeElement> _elementsOnScene = new();
        private MergeElement _currentObj;
        private Coroutine _waitCoroutine;

        private AudioManager _audioManager;
        private InputManager _input;

        void IGameInitListener.OnGameInit()
        {
            _audioManager = GameManager.Instance.GetService<AudioManager>();
            _input = GameManager.Instance.GetService<InputManager>();
        }

        void IGameStartListener.OnGameStart()
        {
            CreatePrefab();
        }

        public void CreatePrefab()
        {
            if (_currentObj != null)
                Destroy(_currentObj.gameObject);
            
            _currentObj = _factory.CreateRandObject();
            SetupBounds();
            SpawnElement(_currentObj, _spawnTransform.position, Quaternion.identity);
            _audioManager.PlayClipByType(ClipType.Spawn);
            _currentObj.transform.SetParent(_spawnTransform);
            _ray.SetActive(true);
        }

        private void SetupBounds()
        {
            Vector3 value = _currentObj.SpriteRendererComponent.bounds.size * 0.5f;
            var valueX = value.x;

            _leftBoard = _leftSpawnPoint.position.x + valueX;
            _rightBoard = _rightSpawnPoint.position.x - valueX;
        }

        void IUpdateListener.Update(float deltaTime)
        {
            // TEST!!!!!
            if (_currentObj == null)
                return;
            
            if (Input.GetKeyDown(KeyCode.S))
                Spawn();

            float x = Input.GetAxis("Horizontal");
            MoveSpawnPoint(x);
        }

        private void MoveSpawnPoint(float f)
        {
            Vector3 newPos = _spawnTransform.position + (_moveSpeed * Time.deltaTime * new Vector3(f, 0, 0));

            newPos.x = Mathf.Clamp(newPos.x, _leftBoard, _rightBoard);
            _spawnTransform.position = newPos;
        }

        public void Spawn()
        {
            if (_currentObj == null)
                return;
            
            _elementsOnScene.Add(_currentObj);
            _currentObj.transform.SetParent(null);
            _currentObj.RigidbodyComponent.simulated = true;
            _currentObj = null;
            _ray.SetActive(false);

            if (_waitCoroutine != null)
                StopCoroutine(_waitCoroutine);
            _waitCoroutine = StartCoroutine(WaitAndSpawn());
            
            IEnumerator WaitAndSpawn()
            {
                yield return new WaitForSeconds(1f);
                CreatePrefab();
            }
        }

        public void CreateBlockBetween(MergeElement elem1, MergeElement elem2)
        {
            Vector3 spawnPoint = (elem1.transform.position + elem2.transform.position) * 0.5f;
            Quaternion spawnRotation = elem1.transform.rotation * elem2.transform.rotation;

            _elementsOnScene.Remove(elem1);
            _elementsOnScene.Remove(elem2);
            
            Destroy(elem1.gameObject);
            Destroy(elem2.gameObject);

            var newElem = _factory.CreateNextGradeObject(elem1);
            if (newElem != null)
            {
                SpawnElement(newElem, spawnPoint, spawnRotation);
                _elementsOnScene.Add(newElem);
                newElem.RigidbodyComponent.simulated = true;
            }
        }

        public void SpawnElement(MergeElement element, Vector3 spawnPoint, Quaternion identity)
        {
            element.transform.position = spawnPoint;
            element.transform.rotation = identity;
        }

        private void ClearScene()
        {
            var spawnPos = _spawnTransform.position;
            spawnPos.x = 0;
            _spawnTransform.position = spawnPos;
            
            foreach (var element in _elementsOnScene)
            {
                Destroy(element.gameObject);
            }
            _elementsOnScene.Clear();
        }

        void IGameFinishListener.OnGameFinished() => ClearScene();

        void IGameRestartListener.OnGameRestarted()
        {
            ClearScene();
            CreatePrefab();
        }
    }
}