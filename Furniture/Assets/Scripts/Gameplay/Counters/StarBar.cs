using System;
using UI;
using UnityEngine;

namespace Gameplay.Counters
{
    [RequireComponent(typeof(UIBar))]
    public class StarBar : MonoBehaviour
    {
        private const float _changingBarSpeed = 5f;

        private UIBar _UIBar;

        private float _currentValue = 1f;
        private float _reachedValue = 1f;

        public static event Action<float> StarBarChanged;

        public void Add(float value)
        {
            _reachedValue += value;
            _reachedValue = Mathf.Clamp01(_reachedValue);
            StarBarChanged?.Invoke(_reachedValue);
        }

        private void Awake()
        {
            _UIBar = GetComponent<UIBar>();
            _UIBar.SetValue(_currentValue);
        }

        private void Update()
        {
            _currentValue = Mathf.Lerp(_currentValue, _reachedValue, Time.deltaTime * _changingBarSpeed);
            _UIBar.SetValue(_currentValue);
        }
    }
}
