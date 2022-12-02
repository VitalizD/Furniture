using Gameplay.Counters;
using Prelevel;
using Service;
using UI.Buttons;
using UnityEngine;

namespace EventHandlers
{
    [RequireComponent(typeof(GameManager))]
    public class GameManagerHandler : MonoBehaviour
    {
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GetComponent<GameManager>();
        }

        private void OnEnable()
        {
            ProgressCounter.LevelFinished += OnFinishLevel;
            RestartButton.RestartButtonPressed += _gameManager.RestartLevel;
            NextLevelButton.NextLevelButtonPressed += _gameManager.NextLevel;
            HomeButton.HomeButtonPressed += _gameManager.ToHome;
            LevelWithTimer.LevelLost += OnLevelLost;
        }

        private void OnDisable()
        {
            ProgressCounter.LevelFinished -= OnFinishLevel;
            RestartButton.RestartButtonPressed -= _gameManager.RestartLevel;
            NextLevelButton.NextLevelButtonPressed -= _gameManager.NextLevel;
            HomeButton.HomeButtonPressed -= _gameManager.ToHome;
            LevelWithTimer.LevelLost -= OnLevelLost;
        }

        private void OnFinishLevel(int starsCount) => StartCoroutine(_gameManager.FinishLevel(starsCount));

        private void OnLevelLost() => StartCoroutine(_gameManager.FinishLevel(0));
    }
}
