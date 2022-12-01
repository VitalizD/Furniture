using Gameplay;
using Prelevel;
using UnityEngine;

namespace EventHandlers
{
    [RequireComponent(typeof(Timer))]
    public class TimerHandler : MonoBehaviour
    {
        private Timer _timer;

        private void Awake()
        {
            _timer = GetComponent<Timer>();
        }

        private void OnEnable()
        {
            LevelWithTimer.StartTimer += _timer.Activate;
        }

        private void OnDisable()
        {
            LevelWithTimer.StartTimer -= _timer.Activate;
        }
    }
}