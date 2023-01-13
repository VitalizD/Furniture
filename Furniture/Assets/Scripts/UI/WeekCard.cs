using TMPro;
using UI.Buttons;
using UnityEngine;

namespace UI
{
    public class WeekCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _starsCountText;
        [SerializeField] private LevelButton[] _levelButtons;

        private int _starsCount = 0;

        public void SetWeekNumber(int value) => _title.text = value.ToString();

        public void SetLevelNumbers(int start)
        {
            var levelNumber = start;
            for (var i = 0; i < _levelButtons.Length; ++i)
                _levelButtons[i].SetNumber(levelNumber++);
        }

        public void SetStarsToLevel(int level, int stars)
        {
            var newStars = stars - _levelButtons[(level % 8) - 1].StarsCount;
            if (newStars <= 0)
                return;
            _levelButtons[(level % 8) - 1].SetStars(stars);
            _starsCount += newStars;
            UpdateStarsCountText();
        }

        public void UnlockLevel(int level)
        {
            _levelButtons[(level % 8) - 1].SetInteractable(true);
        }

        private void Start()
        {
            UpdateStarsCountText();
        }

        private void UpdateStarsCountText() => _starsCountText.text = $"{_starsCount} / 21";
    }
}