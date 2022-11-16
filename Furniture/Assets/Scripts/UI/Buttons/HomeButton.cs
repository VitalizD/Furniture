using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class HomeButton : MonoBehaviour
    {
        private Button _button;

        public static event Action HomeButtonPressed;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Execute);
        }

        private void Execute()
        {
            HomeButtonPressed?.Invoke();
        }
    }

}