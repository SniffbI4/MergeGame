using System;
using Scripts.Game;
using Sirenix.OdinInspector;

namespace Scripts.GamePlay
{
    public class ScoreCurrency : IGameStartListener
    {
        public event Action<int> OnCurrencyChanged;
        public int CurrentCurrency => _current;
        
        private int _current;

        void IGameStartListener.OnGameStart() => SetCurrency(0);

        public void SetCurrency(int newCurrency)
        {
            _current = newCurrency;
            OnCurrencyChanged?.Invoke(_current);
        }

        [Button]
        public void AddCurrency(int icrease)
        {
            _current += icrease;
            OnCurrencyChanged?.Invoke(_current);
        }
    }
}