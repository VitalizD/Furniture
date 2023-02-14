using Service;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ChooseLevelButton : MonoBehaviour
    {
        private Button _button;

        public static event Action ChooseLevelButtonPressed;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnEnable()
        {
            _button.interactable = PlayerPrefs.GetInt(GameManager.TUTORIAL_FINISHED_KEY, 0) == 1;
        }

        private void OnClick()
        {
            ChooseLevelButtonPressed?.Invoke();
        }
    }
}
