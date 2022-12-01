using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private GameObject _timer;
        [SerializeField] private TextMeshProUGUI _valueText;

        private Coroutine _tickCoroutine;

        private int _remainSeconds;

        public static event Action TimeIsUp;

        public void Activate(int seconds)
        {
            _remainSeconds = Mathf.Clamp(seconds, 0, int.MaxValue);
            _timer.SetActive(true);
            _tickCoroutine = StartCoroutine(Tick());
        }

        public void Stop()
        {
            if (_tickCoroutine != null)
                StopCoroutine(_tickCoroutine);
            _timer.SetActive(false);
            _remainSeconds = 0;
        }

        public void Pause()
        {
            if (_tickCoroutine != null)
                StopCoroutine(_tickCoroutine);
        }

        public void Resume()
        {
            _tickCoroutine = StartCoroutine(Tick());
        }

        private IEnumerator Tick()
        {
            if (_remainSeconds < 0)
            {
                Finish();
                yield break;
            }
            UpdateText();
            yield return new WaitForSeconds(1f);
            --_remainSeconds;
            _tickCoroutine = StartCoroutine(Tick());
        }

        private void Finish()
        {
            TimeIsUp?.Invoke();
            _timer.SetActive(false);
        }

        private void UpdateText() => _valueText.text = _remainSeconds.ToString();
    }
}
