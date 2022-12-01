using Gameplay;
using System;
using UnityEngine;

namespace Prelevel
{
    public class LevelWithTimer : MonoBehaviour, IPrelevelExecutable
    {
        [SerializeField] private int _time;

        public static event Action<int> StartTimer;

        public void Execute()
        {
            StartTimer?.Invoke(_time);
        }

        private void OnEnable()
        {
            Timer.TimeIsUp += FinishLevel;
        }

        private void OnDisable()
        {
            Timer.TimeIsUp -= FinishLevel;
        }

        private void FinishLevel()
        {

        }
    }
}
