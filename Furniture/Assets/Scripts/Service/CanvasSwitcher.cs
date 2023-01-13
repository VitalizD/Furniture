using Gameplay;
using UI;
using UnityEngine;

namespace Service
{
    public class CanvasSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject[] _canvases;
        [SerializeField] private GameObject _gameplayCanvas;
        [SerializeField] private LevelResults _levelResultsCanvas;
        [SerializeField] private GameObject _homeCanvas;
        [SerializeField] private GameObject _chooseLevelCanvas;

        public void SwitchToGameplay() => Switch(_gameplayCanvas);

        public void SwitchToLevelResults(string text, int starsCount)
        {
            _levelResultsCanvas.SetText(text);
            Switch(_levelResultsCanvas.gameObject);
            _levelResultsCanvas.StartStarsAnimation(starsCount);
        }

        public void SwitchToHome() => Switch(_homeCanvas);

        public void SwitchToChooseLevel() => Switch(_chooseLevelCanvas);

        private void Switch(GameObject screen)
        {
            foreach (var can in _canvases)
                can.SetActive(false);

            screen.SetActive(true);
        }
    }
}
