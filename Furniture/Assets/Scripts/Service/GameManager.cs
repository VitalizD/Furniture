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

        public bool LoadData { get => _loadData; }

        public static event Action<int> SetGoalForLevel;
        public static event Action LevelStarted;
        public static event Action<int> LevelUnlocked;
        public static event Action<int, int> StarsEarned;
        public static event Action LevelDestroyed;

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

        public void ResetSave() => PlayerPrefs.DeleteAll();

        public void ToHome()
        {
            _canvasSwitcher.SwitchToHome();
        }

        public void ToChooseLevelScreen()
        {
            if (!_tutorialFinished && _lastTutorialLevel > _tutorialLevels.Length)
                FinishTutorial();

            DestroyLevel();
            _canvasSwitcher.SwitchToChooseLevel();
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

        public void StartLevel(int level)
        {
            _canvasSwitcher.SwitchToGameplay();
            var createdLevel = _tutorialFinished ? _levels[level - 1] : _tutorialLevels[level - 1];
            _currentLevelObject = Instantiate(createdLevel.gameObject, Vector3.zero, Quaternion.identity);
            SetGoalForLevel?.Invoke(createdLevel.Goal);
            LevelStarted?.Invoke();
            createdLevel.ExecutePrelevelScripts();
            _currentLevel = level;

            if (_tutorialFinished && _lastLevel < level)
                _lastLevel = level;
        }

        public IEnumerator FinishLevel(int starsCount)
        {
            yield return new WaitForSeconds(starsCount == 0 ? 0f : _delayBeforeLevelFinishing);
            DestroyLevel();
            var resultText = _tutorialFinished ? $"Level {_currentLevel}\ncompleted!" : "Level\ncompleted!";
            if (starsCount == 0)
                resultText = $"Level {_currentLevel}\nnot completed";
            _canvasSwitcher.SwitchToLevelResults(resultText, starsCount);

            if (_tutorialFinished)
            {
                RecalculateStarsCount(_levels[_currentLevel - 1]);
                if (_currentLevel == _lastLevel)
                {
                    ++_lastLevel;
                    LevelUnlocked?.Invoke(_lastLevel);
                    if (_lastLevel % 7 == 0)
                        LevelUnlocked?.Invoke(_lastLevel + 1);
                }
                StarsEarned?.Invoke(_currentLevel, starsCount);
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
            if (_tutorialFinished)
                _canvasSwitcher.SwitchToHome();
            else
                NextLevel();
        }

        private void FinishTutorial()
        {
            _tutorialFinished = true;
            Save();
        }

        private void DestroyLevel()
        {
            if (_currentLevelObject != null)
                Destroy(_currentLevelObject);
            LevelDestroyed?.Invoke();
        }
    }
}
