using Gameplay;
using Prelevel;
using Service;
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
            LevelWithTimer.StopTimer += _timer.Stop;
            GameManager.LevelDestroyed += _timer.Stop;
        }

        private void OnDisable()
        {
            LevelWithTimer.StartTimer -= _timer.Activate;
            LevelWithTimer.StopTimer -= _timer.Stop;
            GameManager.LevelDestroyed -= _timer.Stop;
        }
    }
}