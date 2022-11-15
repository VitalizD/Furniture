using UnityEngine;
using Gameplay.Furniture;
using Gameplay.Counters;

namespace EventHandlers
{
    [RequireComponent(typeof(ProgressCounter))]
    public class ProgressCounterHandler : MonoBehaviour
    {
        private ProgressCounter _progresCounter;

        private void Awake()
        {
            _progresCounter = GetComponent<ProgressCounter>();
        }

        private void OnEnable()
        {
            FurniturePlace.FurnitureInPlaced += Add;
        }

        private void OnDisable()
        {
            FurniturePlace.FurnitureInPlaced -= Add;
        }

        private void Add(int count) => _progresCounter.Add(count);
    }
}