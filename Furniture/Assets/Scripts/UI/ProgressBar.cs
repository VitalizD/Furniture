using UnityEngine;
using Gameplay.Furniture;

namespace UI
{
    [RequireComponent(typeof(UIBar))]
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private FurniturePlace[] _places;

        private UIBar _UIBar;

        private int _placesCount;
        private int _inPlaces = 0;

        public void Add(int count)
        {
            _inPlaces = Mathf.Clamp(_inPlaces + count, 0, _placesCount);
            _UIBar.SetValue((float)_inPlaces / _placesCount * 100f);
        }

        private void Awake()
        {
            _placesCount = _places.Length;
            _UIBar = GetComponent<UIBar>();
            _UIBar.SetValue(0);
        }
    }
}
