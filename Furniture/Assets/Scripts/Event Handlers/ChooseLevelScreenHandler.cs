using Service;
using UI;
using UnityEngine;

namespace EventHandlers
{
    [RequireComponent(typeof(ChooseLevelScreen))]
    public class ChooseLevelScreenHandler : MonoBehaviour
    {
        private ChooseLevelScreen _chooseLevelScreen;

        private void Awake()
        {
            _chooseLevelScreen = GetComponent<ChooseLevelScreen>();
        }

        private void OnEnable()
        {
            GameManager.LevelUnlocked += _chooseLevelScreen.UnlockLevel;
            GameManager.StarsEarned += _chooseLevelScreen.SetStars;
        }

        private void OnDisable()
        {
            GameManager.LevelUnlocked -= _chooseLevelScreen.UnlockLevel;
            GameManager.StarsEarned -= _chooseLevelScreen.SetStars;
        }
    }
}