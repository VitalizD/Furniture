using Gameplay.Furniture;
using UnityEngine;

namespace Service
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField] private FurniturePlace[] _furniturePlaces;

        public int Goal { get => _furniturePlaces.Length; }

        private int _starsCount = 0;

        public void SetStars(int value) => _starsCount = value;
    }
}
