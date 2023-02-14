using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Counters
{
    [RequireComponent(typeof(Animation))]
    public class Star : MonoBehaviour
    {
        private const string _deactivateAnimationName = "Deactivate";
        private const string _appearanceAnimationName = "Appearance";

        [SerializeField] private Image _filled;
        [SerializeField] private float _barValueToDeactivate;

        private Animation _animation;

        private bool _isActive = true;

        public static event Action StarLost;

        public void SetFilledPartActive(bool value) => _filled.gameObject.SetActive(value);

        public void PlayAppearance()
        {
            _isActive = true;
            _animation.Play(_appearanceAnimationName);
        }

        public void SetVisibility(bool value) => _filled.gameObject.SetActive(value);

        public void CheckState(float barValue)
        {
            if (!_isActive)
                return;

            if (barValue <= _barValueToDeactivate)
            {
                Deactivate();
                StarLost?.Invoke();
            }
        }

        private void Awake()
        {
            _animation = GetComponent<Animation>();
        }

        private void Deactivate()
        {
            _isActive = false;
            _animation.Play(_deactivateAnimationName);
        }
    }
}
