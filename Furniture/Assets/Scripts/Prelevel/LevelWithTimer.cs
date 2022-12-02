using Gameplay;
using Gameplay.Counters;
using System;
using UnityEngine;

namespace Prelevel
{
    public class LevelWithTimer : MonoBehaviour, IPrelevelExecutable
    {
        [SerializeField] private int _time;

        public static event Action<int> StartTimer;
        public static event Action StopTimer;
        public static event Action LevelLost;

        public void Execute()
        {
            StartTimer?.Invoke(_time);
        }

        private void OnEnable()
        {
            Timer.TimeIsUp += DefeatLevel;
            ProgressCounter.LevelFinished += RemoveTimer;
        }

        private void OnDisable()
        {
            Timer.TimeIsUp -= DefeatLevel;
            ProgressCounter.LevelFinished -= RemoveTimer;
        }

        private void DefeatLevel()
        {
            LevelLost?.Invoke();
        }

        private void RemoveTimer(int arg)
        {
            StopTimer?.Invoke();
        }
    }
}
