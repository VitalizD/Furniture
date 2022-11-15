using UnityEngine;
using Gameplay.Furniture;
using TMPro;

namespace Gameplay.Counters
{
    public class ProgressCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private FurniturePlace[] _places;

        private int _placesCount;
        private int _inPlaces = 0;

        public void Add(int count)
        {
            _inPlaces = Mathf.Clamp(_inPlaces + count, 0, _placesCount);
            UpdateText();
        }

        private void Awake()
        {
            _placesCount = _places.Length;
            UpdateText();
        }

        private void UpdateText() => _text.text = $"{_inPlaces} / {_placesCount}";
    }
}
