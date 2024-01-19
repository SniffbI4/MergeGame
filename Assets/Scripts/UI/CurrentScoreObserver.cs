using DG.Tweening;
using Scripts.GamePlay;
using Scripts.Game;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class CurrentScoreObserver : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private float _textScaleMultiplier = 1.2f;
        [SerializeField] private float _scaleDuration = 0.2f;
        [SerializeField] private float _changeTextDuration = 0.5f;

        private ScoreCurrency _scoreCurrency;
        private int _prevScore;
        private Sequence _sequence;

        private void Start()
        {
            _scoreCurrency = GameManager.Instance.GetService<ScoreCurrency>();
            _scoreCurrency.OnCurrencyChanged += UpdateScoreText;

            _prevScore = _scoreCurrency.CurrentCurrency;
            _scoreText.text = _prevScore.ToString();
        }

        private void OnDestroy()
        {
            _scoreCurrency.OnCurrencyChanged -= UpdateScoreText;
        }

        private void UpdateScoreText(int newScore)
        {
            if (newScore == 0)
                Setter(newScore);
            else
                AnimateText(_prevScore, newScore);
            
            _prevScore = newScore;
        }

        private void AnimateText(int lastScore, int newScore)
        {
            _sequence?.Kill();

            _sequence = DOTween.Sequence();
            _textScaleMultiplier = 1.2f;
            _scaleDuration = 0.2f;
            _sequence.Append(_scoreText.transform.DOScale(_textScaleMultiplier, _scaleDuration));
            _changeTextDuration = 0.5f;
            var tweenCore = DOTween.To(() => lastScore, Setter, newScore, _changeTextDuration);
            _sequence.Append(tweenCore);
            _sequence.Append(_scoreText.transform.DOScale(Vector3.one, _scaleDuration));
        }

        private void Setter(int value)
        {
            _scoreText.text = value.ToString();
        }
    }
}