using System;
using System.Collections.Generic;
using FloatingText;
using Scripts.GamePlay;
using Scripts.SaveLoad;
using Scripts.Audio;
using Scripts.Progress;
using Scripts.UI;
using Scripts.UserInput;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Space(25)]
        [Header("Configs")]
        [SerializeField] private GameProgress _progressConfig;

        [Space(25)]
        [Header("MonoServices")]
        [SerializeField] private GameFlowInstaller _gameFlowInstaller;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private InputManager _input;
        [SerializeField] private ObjectSpawner _spawner;
        [SerializeField] private Canvas _mainCanvas;

        [Space(30)]
        [ShowInInspector]
        private GameFlow _gameFlow;
        
        [ShowInInspector]
        public Dictionary<Type, object> _services = new();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            RegisterServices();
            
            _gameFlowInstaller.InstallGameListeners(_gameFlow);
            RegisterNonMonoListeners();
            
            _gameFlow.InitGame();
            _gameFlow.StartGame();
        }

        private void Update()
        {
            _gameFlow.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _gameFlow.FixedUpdate(Time.fixedDeltaTime);
        }

        private void RegisterNonMonoListeners()
        {
            foreach (var service in _services.Values)
            {
                if (service is IGameListener listener)
                {
                    _gameFlow.AddListener(listener);
                }
            }
        }

        public T GetService<T>() where T : class
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                throw new Exception($"Service {typeof(T).Name} is not found!");
            }
            
            return _services[typeof(T)] as T;
        }

        private void RegisterServices()
        {
            //independent
            _services.Add(typeof(SaveLoadManager), new SaveLoadManager());
            _services.Add(typeof(ScoreCurrency), new ScoreCurrency());
            _services.Add(typeof(AudioManager), _audioManager);
            _services.Add(typeof(InputManager), _input);
            _services.Add(typeof(FloatingTextController), new FloatingTextController(_mainCanvas));

            //with dependencies
            _services.Add(typeof(GameProgressController), new GameProgressController(_progressConfig));
            _services.Add(typeof(ObjectSpawner), _spawner);
            _services.Add(typeof(UIManager), _uiManager);

            _gameFlow = new GameFlow();
            _services.Add(typeof(GameFlow), _gameFlow);
        }
    }
}