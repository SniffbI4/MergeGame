using Scripts.Audio;
using Scripts.Game;
using Scripts.GamePlay;
using Scripts.SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class UIManager : MonoBehaviour,
                             IGameInitListener,
                             IGameStartListener,
                             IGamePauseListener,
                             IGameResumeListener,
                             IGameFinishListener
    {
        [Header("Panels")]
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _finishPanel;
        
        [Header("Buttons")]
        [SerializeField] private ButtonComponent _soundButton;
        [SerializeField] private ButtonComponent _restartGameButton;
        [SerializeField] private ButtonComponent _restartAfterFinishButton;
        [SerializeField] private Button _pausePanelButton;

        [Header("Record")]
        [SerializeField] private TMP_Text _recordText;
        [SerializeField] private TMP_Text _finishText;
        
        private AudioManager _audioManager;
        private GameFlow _gameFlow;
        private SaveLoadManager _saveLoadManager;
        private ScoreCurrency _scoreCurrency;

        void IGameInitListener.OnGameInit()
        {
            _audioManager = GameManager.Instance.GetService<AudioManager>();
            _gameFlow = GameManager.Instance.GetService<GameFlow>();
            _saveLoadManager = GameManager.Instance.GetService<SaveLoadManager>();
            _scoreCurrency = GameManager.Instance.GetService<ScoreCurrency>();
            
            _soundButton.OnButtonPressedWithState += OnSoundButtonPressed;
            _restartGameButton.OnButtonPressed += _gameFlow.RestartGame;
            _restartAfterFinishButton.OnButtonPressed += _gameFlow.RestartGame;
            _pausePanelButton.onClick.AddListener(_gameFlow.ResumeGame);
        }
        
        private void OnDestroy()
        {
            _soundButton.OnButtonPressedWithState -= OnSoundButtonPressed;
            _restartGameButton.OnButtonPressed -= _gameFlow.RestartGame;
            _restartAfterFinishButton.OnButtonPressed -= _gameFlow.RestartGame;
            _pausePanelButton.onClick.RemoveAllListeners();
        }

        void IGameStartListener.OnGameStart()
        {
            UpdateRecordText(_saveLoadManager.LoadScoreRecord().ToString());
            HidePausePanel();
            HideFinishGamePanel();
        }

        void IGamePauseListener.OnGamePaused() => ShowPausePanel();

        void IGameResumeListener.OnGameResumed() => HidePausePanel();

        void IGameFinishListener.OnGameFinished()
        {
            int currentRecord = _saveLoadManager.LoadScoreRecord();
            int currentScore = _scoreCurrency.CurrentCurrency;

            if (currentScore > currentRecord)
            {
                _saveLoadManager.SaveScoreRecord(currentScore);
                UpdateRecordText(currentScore.ToString());
            }
            
            ShowFinishGamePanel();
        }

        private void OnSoundButtonPressed(bool state) => 
                _audioManager.SetMusicMuteState(isMute: state);

        public void ShowPausePanel() => 
                _pausePanel.SetActive(true);

        public void HidePausePanel() => 
                _pausePanel.SetActive(false);

        public void ShowFinishGamePanel()
        {
            _finishText.text = "ВАШ СЧЕТ";
            _finishPanel.SetActive(true);
        }

        public void HideFinishGamePanel()
        {
            if (_finishPanel.activeInHierarchy)
                _finishPanel.SetActive(false);
        }

        public void UpdateRecordText(string newRecord)
        {
            _finishText.text = "НОВЫЙ РЕКОРД";
            _recordText.text = newRecord;
        }
    }
}