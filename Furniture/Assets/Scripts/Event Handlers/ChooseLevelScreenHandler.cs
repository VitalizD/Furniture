using Service;
using UI;
using UI.Buttons;
using UnityEngine;

namespace EventHandlers
{
    [RequireComponent(typeof(ChooseLevelScreen))]
    public class ChooseLevelScreenHandler : MonoBehaviour
    {
        [SerializeField] private WeekCardSwiping _weekCardSwiping;

        private ChooseLevelScreen _chooseLevelScreen;

        private void Awake()
        {
            _chooseLevelScreen = GetComponent<ChooseLevelScreen>();
        }

        private void OnEnable()
        {
            GameManager.LevelUnlocked += _chooseLevelScreen.UnlockLevel;
            GameManager.StarsEarned += _chooseLevelScreen.SetStars;
            WeekSwitch.GetWeekCardSwiping += GetWeekCardSwiping;
        }

        private void OnDisable()
        {
            GameManager.LevelUnlocked -= _chooseLevelScreen.UnlockLevel;
            GameManager.StarsEarned -= _chooseLevelScreen.SetStars;
            WeekSwitch.GetWeekCardSwiping -= GetWeekCardSwiping;
        }

        private WeekCardSwiping GetWeekCardSwiping() => _weekCardSwiping;
    }
}