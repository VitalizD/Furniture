using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ChooseLevelButton : MonoBehaviour
    {
        public static event Action ChooseLevelButtonPressed;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            ChooseLevelButtonPressed?.Invoke();
        }
    }
}
