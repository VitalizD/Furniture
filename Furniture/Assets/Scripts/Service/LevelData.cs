using Gameplay.Furniture;
using UnityEngine;

namespace Service
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField] private FurniturePlace[] _furniturePlaces;

        public int Goal { get => _furniturePlaces.Length; }

        public int StarsCount { get; set; } = 0;
    }
}
