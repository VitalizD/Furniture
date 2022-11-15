using Gameplay.Furniture;
using System;
using UnityEngine;

namespace EventHandlers
{
    public class ImpactManagerHandler : MonoBehaviour
    {
        [SerializeField] private int _maxFurnitureImpactsCount;

        public static event Action<float> AddToStarBar;

        private void OnEnable()
        {
            FurnitureMoving.FurnitureCollided += OnFurnitureCollided;
        }

        private void OnDisable()
        {
            FurnitureMoving.FurnitureCollided -= OnFurnitureCollided;
        }

        private void OnFurnitureCollided()
        {
            AddToStarBar?.Invoke(-1f / _maxFurnitureImpactsCount);
        }
    }
}
