using Gameplay.Counters;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _number;
        [SerializeField] private Star[] _stars;

        private Button _button;
        private int _levelNumber;

        public int StarsCount { get; private set; } = 0;

        public static event Action<int> LevelButtonPressed;

        public void SetNumber(int value)
        {
            _levelNumber = value;
            _number.text = value.ToString();
        }

        public void SetStars(int count)
        {
            StarsCount = Mathf.Clamp(count, 0, _stars.Length);
            for (var i = 0; i < StarsCount; ++i)
                _stars[i].PlayAppearance();
        }

        public void SetInteractable(bool value) => _button.interactable = value;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            LevelButtonPressed?.Invoke(_levelNumber);
        }
    }
}