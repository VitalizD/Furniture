using UnityEngine;
using UnityEngine.UI;
using Gameplay.Furniture;
using TMPro;

public class ForceSlider : MonoBehaviour
{
    [SerializeField] private FurnitureMoving[] _furnitures;
    [SerializeField] private TextMeshProUGUI _text;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void ChangeForce()
    {
        foreach (var furniture in _furnitures)
            furniture.SetForce(_slider.value);
        _text.text = _slider.value.ToString();
    }
}
