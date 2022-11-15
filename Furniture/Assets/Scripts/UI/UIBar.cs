using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class UIBar : MonoBehaviour
    {
        [SerializeField] private Image bar;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private TextMeshProUGUI title;

        public float Value { get => bar.fillAmount; }

        public bool Filled { get => bar.fillAmount >= 0.99f; }

        public bool Empty { get => bar.fillAmount <= 0.01f; }

        /// <param name="value">От 0 до 1</param>
        public void SetValue(float value)
        {
            value = Mathf.Clamp(value, 0f, 1f);
            bar.fillAmount = value;
            if (valueText != null)
                valueText.text = $"{(int)value} %";
        }

        public void SetTitle(string value)
        {
            if (title == null)
                return;

            title.text = value;
        }
    }
}