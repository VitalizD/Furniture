using Gameplay.Counters;
using UnityEngine;

namespace EventHandlers
{
    [RequireComponent(typeof(Star))]
    public class StarHandler : MonoBehaviour
    {
        private Star _star;

        private void Awake()
        {
            _star = GetComponent<Star>();
        }

        private void OnEnable()
        {
            StarBar.StarBarChanged += _star.CheckState;
        }

        private void OnDisable()
        {
            StarBar.StarBarChanged -= _star.CheckState;
        }
    }
}
