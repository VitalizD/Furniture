using Service;
using UI;
using UnityEngine;

namespace EventHandlers
{
    [RequireComponent(typeof(GameplayScreen))]
    public class GameplayScreenHandler : MonoBehaviour
    {
        private GameplayScreen _gameplayScreen;

        private void Awake()
        {
            _gameplayScreen = GetComponent<GameplayScreen>();
        }

        private void OnEnable()
        {
            GameManager.TutorialFinished += ActivateHomeButton;
        }

        private void OnDisable()
        {
            GameManager.TutorialFinished -= ActivateHomeButton;
        }

        private void ActivateHomeButton() => _gameplayScreen.SetActiveHomeButton(true);
    }
}