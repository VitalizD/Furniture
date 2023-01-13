using System.Collections;
using UnityEngine;

namespace UI
{
    public class ChooseLevelScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private WeekCard[] _weekCards;

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

        private void DistributeLevels()
        {
            for (var i = 0; i < _weekCards.Length; ++i)
                _weekCards[i].SetLevelNumbers(i * 7 + 1);
        }

        private IEnumerator Initialize()
        {
            _content.SetActive(true);
            yield return new WaitForEndOfFrame();
            DistributeLevels();
            _weekCards[0].UnlockLevel(1);
            _content.SetActive(false);
        }

        private bool LevelExists(int level)
        {
            return (level - 1) / 7 < _weekCards.Length;
        }
    }
}
