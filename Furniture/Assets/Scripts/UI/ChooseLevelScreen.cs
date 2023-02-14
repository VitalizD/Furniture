using System.Collections;
using UnityEngine;

namespace UI
{
    public class ChooseLevelScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private GameObject _weekSwitchPrefab;
        [SerializeField] private Transform _switchers;
        [SerializeField] private WeekCard[] _weekCards;
        [SerializeField] private bool _specialLastCard = false;

        public void UnlockLevel(int level)
        {
            if (!LevelExists(level)) return;
            _weekCards[(level - 1) / 7].UnlockLevel(level);
        }

        public void SetStars(int level, int stars)
        {
            if (!LevelExists(level)) return;
            _weekCards[(level - 1) / 7].SetStarsToLevel(level, stars);
        }

        private void Start()
        {
            StartCoroutine(Initialize());
        }

        private void InitializeWeekCards()
        {
            for (var i = 0; i < _weekCards.Length; ++i)
            {
                Instantiate(_weekSwitchPrefab, _switchers);
                _weekCards[i].SetWeekNumber(i + 1);

                if (i == _weekCards.Length - 1 && _specialLastCard)
                    break;

                _weekCards[i].SetLevelNumbers(i * 7 + 1);
            }
        }

        private IEnumerator Initialize()
        {
            _content.SetActive(true);
            yield return new WaitForEndOfFrame();
            InitializeWeekCards();
            _weekCards[0].UnlockLevel(1);
            _content.SetActive(false);
        }

        private bool LevelExists(int level)
        {
            return (level - 1) / 7 < (_specialLastCard ? _weekCards.Length - 1 : _weekCards.Length);
        }
    }
}
