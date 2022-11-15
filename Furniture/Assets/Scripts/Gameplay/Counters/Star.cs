using System;
using UnityEngine;

namespace Gameplay.Counters
{
    [RequireComponent(typeof(Animation))]
    public class Star : MonoBehaviour
    {
        [SerializeField] private float _barValueToDeactivate;

        private Animation _animation;

        private bool _isActive = true;

        public static event Action StarLost;

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
            _animation.Play();
        }
    }
}
