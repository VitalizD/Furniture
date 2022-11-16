using Gameplay.Counters;
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
        }

        private void OnDisable()
        {
            ProgressCounter.LevelFinished -= OnFinishLevel;
            RestartButton.RestartButtonPressed -= _gameManager.RestartLevel;
            NextLevelButton.NextLevelButtonPressed -= _gameManager.NextLevel;
            HomeButton.HomeButtonPressed -= _gameManager.ToHome;
        }

        private void OnFinishLevel(int starsCount) => StartCoroutine(_gameManager.FinishLevel(starsCount));
    }
}
