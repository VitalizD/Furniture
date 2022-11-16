using Gameplay.Counters;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class LevelResults : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelCompletedText;
        [SerializeField] private Star[] _stars;
        [SerializeField] private float _delayBetweenStars;

        public void SetText(string value) => _levelCompletedText.text = value;

        public void StartStarsAnimation(int count)
        {
            count = Mathf.Clamp(count, 0, _stars.Length);

            if (count == 0)
                return;

            StartCoroutine(ShowStar(0, count));
        }

        private void OnEnable()
        {
            foreach (var star in _stars)
                star.SetFilledPartActive(false);
        }

        private IEnumerator ShowStar(int index, int count)
        {
            _stars[index].PlayAppearance();

            if (index == count - 1)
                yield break;

            yield return new WaitForSeconds(_delayBetweenStars);

            StartCoroutine(ShowStar(index + 1, count));
        }
    }
}