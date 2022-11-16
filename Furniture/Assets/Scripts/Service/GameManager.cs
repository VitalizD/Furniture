using System;
using UnityEngine;

namespace Service
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelData[] _tutorialLevels;
        [SerializeField] private LevelData[] _levels;
        [SerializeField] private int _level = 1;
        [SerializeField] private int _tutorialLevel = 1;
        [SerializeField] private bool _tutorialFinished = false;

        public static event Action<int> SetGoalForLevel;

        private void Start()
        {
            NextLevel();
        }

        private void NextLevel()
        {
            var createdLevel = _tutorialFinished ? _levels[_level - 1] : _tutorialLevels[_tutorialLevel - 1];
            Instantiate(createdLevel.gameObject, Vector3.zero, Quaternion.identity);
            SetGoalForLevel?.Invoke(createdLevel.Goal);
        }
    }
}
