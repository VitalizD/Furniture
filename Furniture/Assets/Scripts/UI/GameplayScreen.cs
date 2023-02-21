using UnityEngine;
using Service;

namespace UI
{
    public class GameplayScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _homeButton;

        public void SetActiveHomeButton(bool value) => _homeButton.SetActive(value);

        private void Awake()
        {
            var tutorialDone = PlayerPrefs.GetInt(GameManager.TUTORIAL_FINISHED_KEY, 0) == 1;
            SetActiveHomeButton(tutorialDone);
        }
    }
}