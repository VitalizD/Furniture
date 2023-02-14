using System;
using TMPro;
using UI.Buttons;
using UnityEngine;

namespace UI
{
    public class WeekCard : MonoBehaviour, IStorable
    {
        private const string WEEK_NUMBER_KEY = "WEEK_NUMBER_";
        private const string LEVEL_STARS_KEY = "LEVEL_STARS_";
        private const string LEVEL_UNLOCKED_KEY = "LEVEL_UNLOCKED_";

        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _starsCountText;
        [SerializeField] private LevelButton[] _levelButtons;

        private int _weekNumber;
        private int _starsCount = 0;

        public static event Func<bool> LoadData;

        public void Save()
        {
            foreach (var levelButton in _levelButtons)
            {
                PlayerPrefs.SetInt(LEVEL_STARS_KEY + levelButton.Number, levelButton.StarsCount);
                PlayerPrefs.SetInt(LEVEL_UNLOCKED_KEY + levelButton.Number, levelButton.GetInteractable() ? 1 : 0);
            }
        }

        public void Load() 
        {
            foreach (var levelButton in _levelButtons)
            {
                SetStarsToLevelUnsafe(levelButton.Number, PlayerPrefs.GetInt(LEVEL_STARS_KEY + levelButton.Number, 0));
                levelButton.SetInteractable(PlayerPrefs.GetInt(LEVEL_UNLOCKED_KEY + levelButton.Number, 0) == 1);
            }
        }

        public void SetWeekNumber(int value)
        {
            _weekNumber = value;
            _title.text = $"Неделя {value}";
        }

        public void SetLevelNumbers(int start)
        {
            var levelNumber = start;
            for (var i = 0; i < _levelButtons.Length; ++i)
                _levelButtons[i].SetNumber(levelNumber++);
            if (LoadData())
                Load();
        }

        public void SetStarsToLevel(int level, int stars)
        {
            SetStarsToLevelUnsafe(level, stars);
            Save();
        }

        public void SetStarsToLevelUnsafe(int level, int stars)
        {
            var newStars = stars - _levelButtons[(level - 1) % 7].StarsCount;
            if (newStars <= 0)
                return;
            _levelButtons[(level - 1) % 7].SetStars(stars);
            _starsCount += newStars;
            UpdateStarsCountText();
        }

        public void UnlockLevel(int level)
        {
            _levelButtons[(level - 1) % 7].SetInteractable(true);
            Save();
        }

        private void Start()
        {
            UpdateStarsCountText();
        }

        private void UpdateStarsCountText() => _starsCountText.text = $"{_starsCount} / 21";
    }
}