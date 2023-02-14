using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class WeekSwitch : MonoBehaviour
    {
        private Button _button;

        public static event Func<WeekCardSwiping> GetWeekCardSwiping;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => GetWeekCardSwiping().WhichBtnClicked(_button));
        }
    }
}