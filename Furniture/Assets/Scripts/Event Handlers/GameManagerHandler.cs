using Gameplay.Counters;
using Prelevel;
using Service;
using UI;
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
            PlayButton.PlayButtonPressed += _gameManager.NextLevel;
            HomeButton.HomeButtonPressed += _gameManager.ToHome;
            LevelWithTimer.LevelLost += OnLevelLost;
            LevelButton.LevelButtonPressed += _gameManager.StartLevel;
            ChooseLevelButton.ChooseLevelButtonPressed += _gameManager.ToChooseLevelScreen;
            WeekCard.LoadData += CanLoadData;
        }

        private void OnDisable()
        {
            ProgressCounter.LevelFinished -= OnFinishLevel;
            RestartButton.RestartButtonPressed -= _gameManager.RestartLevel;
            NextLevelButton.NextLevelButtonPressed -= _gameManager.NextLevel;
            PlayButton.PlayButtonPressed -= _gameManager.NextLevel;
            HomeButton.HomeButtonPressed -= _gameManager.ToHome;
            LevelWithTimer.LevelLost -= OnLevelLost;
            LevelButton.LevelButtonPressed -= _gameManager.StartLevel;
            ChooseLevelButton.ChooseLevelButtonPressed -= _gameManager.ToChooseLevelScreen;
            WeekCard.LoadData -= CanLoadData;
        }

        private void OnFinishLevel(int starsCount) => StartCoroutine(_gameManager.FinishLevel(starsCount));

        private void OnLevelLost() => StartCoroutine(_gameManager.FinishLevel(0));

        private bool CanLoadData() => _gameManager.LoadData;
    }
}
