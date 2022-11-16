using UnityEngine;
using Gameplay.Furniture;
using Gameplay.Counters;
using Service;

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
            FurniturePlace.FurnitureInPlaced += AddToFurnitureCounter;
            Star.StarLost += LoseStar;
            GameManager.SetGoalForLevel += _progresCounter.SetGoal;
        }

        private void OnDisable()
        {
            FurniturePlace.FurnitureInPlaced -= AddToFurnitureCounter;
            Star.StarLost -= LoseStar;
            GameManager.SetGoalForLevel -= _progresCounter.SetGoal;
        }

        private void AddToFurnitureCounter(int count) => _progresCounter.AddToFurnitureCounter(count);

        private void LoseStar() => _progresCounter.AddStars(-1);
    }
}