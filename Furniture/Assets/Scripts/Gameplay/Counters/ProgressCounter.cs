using UnityEngine;
using Gameplay.Furniture;
using TMPro;
using System;

namespace Gameplay.Counters
{
    public class ProgressCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private FurniturePlace[] _places;

        private int _placesCount;
        private int _inPlaces = 0;
        private int _starsCount = 3;

        public static event Action<int> LevelFinished;

        public void SetGoal(int value)
        {
            _placesCount = value;
            UpdateText();
        }

        public void AddStars(int count) => _starsCount = Mathf.Clamp(_starsCount += count, 1, 3);

        public void AddToFurnitureCounter(int count)
        {
            _inPlaces = Mathf.Clamp(_inPlaces + count, 0, _placesCount);
            UpdateText();

            if (_inPlaces >= _placesCount)
                LevelFinished?.Invoke(_starsCount);
        }

        private void Awake()
        {
            _placesCount = _places.Length;
            UpdateText();
        }

        private void UpdateText() => _text.text = $"{_inPlaces} / {_placesCount}";
    }
}
