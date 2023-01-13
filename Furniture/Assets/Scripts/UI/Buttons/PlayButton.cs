using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class PlayButton : MonoBehaviour
    {
        private Button _button;

        public static event Action PlayButtonPressed;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            PlayButtonPressed?.Invoke();
        }
    }
}