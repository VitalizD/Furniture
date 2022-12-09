using System;
using System.Collections;
using UnityEngine;

namespace Service
{
    [RequireComponent(typeof(CanvasSwitcher))]
    public class GameManager : MonoBehaviour, IStorable
    {
        public const string TUTORIAL_FINISHED_KEY = "TUTORIAL_FINISHED";
        public const string LAST_TUTORIAL_LEVEL_KEY = "LAST_TUTORIAL_LEVEL";
        public const string LAST_LEVEL_KEY = "LAST_LEVEL";

        [SerializeField] private LevelData[] _tutorialLevels;
        [SerializeField] private LevelData[] _levels;
        [SerializeField] private int _lastLevel = 1;
        [SerializeField] private int _lastTutorialLevel = 1;
        [SerializeField] private bool _tutorialFinished = false;
        [SerializeField] private bool _loadData = false;
        [SerializeField] private float _delayBeforeLevelFinishing;

        private CanvasSwitcher _canvasSwitcher;

        private GameObject _currentLevelObject;
        private int _currentLevel;
        private int _starsCount = 0;

        public static event Action<int> SetGoalForLevel;
        public static event Action LevelStarted;

        public void Save()
        {
            PlayerPrefs.SetInt(TUTORIAL_FINISHED_KEY, _tutorialFinished? 1 : 0);
            PlayerPrefs.SetInt(LAST_LEVEL_KEY, _lastLevel);
            PlayerPrefs.SetInt(LAST_TUTORIAL_LEVEL_KEY, _lastTutorialLevel);
        }

        public void Load()
        {
            _tutorialFinished = PlayerPrefs.GetInt(TUTORIAL_FINISHED_KEY, 0) == 1;
            _lastLevel = PlayerPrefs.GetInt(LAST_LEVEL_KEY, 1);
            _lastTutorialLevel = PlayerPrefs.GetInt(LAST_TUTORIAL_LEVEL_KEY, 1);
        }

        public void ToHome()
        {
            if (!_tutorialFinished && _lastTutorialLevel > _tutorialLevels.Length)
                FinishTutorial();
        }

        public void NextLevel()
        {
            if (!_tutorialFinished && _lastTutorialLevel > _tutorialLevels.Length)
                FinishTutorial();

            if (_tutorialFinished && _lastLevel > _levels.Length)
                _lastLevel = 1;

            StartLevel(_tutorialFinished ? _lastLevel : _lastTutorialLevel);
        }

        public void RestartLevel()
        {
            DestroyLevel();
            StartLevel(_currentLevel);
        }

        public IEnumerator FinishLevel(int starsCount)
        {
            yield return new WaitForSeconds(starsCount == 0 ? 0f : _delayBeforeLevelFinishing);
            DestroyLevel();
            var resultText = _tutorialFinished ? $"Уровень {_currentLevel}\nпройден!" : "Уровень\nпройден!";
            if (starsCount == 0)
                resultText = $"Уровень {_currentLevel}\nне пройден";
            _canvasSwitcher.SwitchToLevelResults(resultText, starsCount);

            if (_tutorialFinished)
            {
                RecalculateStarsCount(_levels[_currentLevel - 1]);
                if (_currentLevel == _lastLevel)
                    ++_lastLevel;
            }
            else
            {
                RecalculateStarsCount(_tutorialLevels[_currentLevel - 1]);
                if (_currentLevel == _lastTutorialLevel)
                    ++_lastTutorialLevel;
            }

            void RecalculateStarsCount(LevelData inLevel)
            {
                _starsCount += Mathf.Clamp(starsCount - inLevel.StarsCount, 0, 3);
                inLevel.StarsCount = starsCount;
            }

            Save();
        }

        private void Awake()
        {
            _canvasSwitcher = GetComponent<CanvasSwitcher>();

            if (_loadData)
                Load();
        }

        private void Start()
        {
            NextLevel();
        }

        private void StartLevel(int level)
        {
            _canvasSwitcher.SwitchToGameplay();
            var createdLevel = _tutorialFinished ? _levels[level - 1] : _tutorialLevels[level - 1];
            _currentLevelObject = Instantiate(createdLevel.gameObject, Vector3.zero, Quaternion.identity);
            SetGoalForLevel?.Invoke(createdLevel.Goal);
            LevelStarted?.Invoke();
            createdLevel.ExecutePrelevelScripts();
            _currentLevel = level;
        }

        private void FinishTutorial()
        {
            _tutorialFinished = true;
        }

        private void DestroyLevel()
        {
            if (_currentLevelObject != null)
                Destroy(_currentLevelObject);
        }
    }
}
