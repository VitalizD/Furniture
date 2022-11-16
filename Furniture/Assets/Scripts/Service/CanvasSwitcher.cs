using Gameplay;
using UnityEngine;

namespace Service
{
    public class CanvasSwitcher : MonoBehaviour
    {
        [SerializeField] private Canvas[] _canvases;
        [SerializeField] private Canvas _gameplayCanvas;
        [SerializeField] private LevelResults _levelResultsCanvas;

        public void SwitchToGameplay() => Switch(_gameplayCanvas);

        public void SwitchToLevelResults(string text, int starsCount)
        {
            _levelResultsCanvas.SetText(text);
            Switch(_levelResultsCanvas.GetComponent<Canvas>());
            _levelResultsCanvas.StartStarsAnimation(starsCount);
        }

        private void Switch(Canvas canvas)
        {
            foreach (var can in _canvases)
                can.gameObject.SetActive(false);

            canvas.gameObject.SetActive(true);
        }
    }
}
