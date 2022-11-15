using UnityEngine;
using UnityEngine.UI;
using Gameplay.Furniture;

public class WightToggle : MonoBehaviour
{
    [SerializeField] private FurnitureMoving[] _furnitures;

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(ChangeWeight);
    }

    public void ChangeWeight(bool arg)
    {
        foreach (var furniture in _furnitures)
        {
            if (arg)
                furniture.SetSameWeight();
            else
                furniture.ReturnWeight();
        }
    }
}
