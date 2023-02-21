using Gameplay;
using Service;
using UnityEngine;

namespace EventHandlers
{
    [RequireComponent(typeof(LevelResults))]
    public class LevelResultsHandler : MonoBehaviour
    {
        private LevelResults _levelResults;

        private void Awake()
        {
            _levelResults = GetComponent<LevelResults>();
        }

        private void OnEnable()
        {
            GameManager.TutorialFinished += ActivateHomeButton;
        }

        private void OnDisable()
        {
            GameManager.TutorialFinished -= ActivateHomeButton;
        }

        private void ActivateHomeButton() => _levelResults.SetActiveHomeButton(true);
    }
}