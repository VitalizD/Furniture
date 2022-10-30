using UnityEngine;
using Gameplay.Furniture;
using UI;

namespace EventHandlers
{
    [RequireComponent(typeof(ProgressBar))]
    public class UpdateProgressBarHandler : MonoBehaviour
    {
        private ProgressBar _progressBar;

        private void Awake()
        {
            _progressBar = GetComponent<ProgressBar>();
        }

        private void OnEnable()
        {
            FurniturePlace.FurnitureInPlaced += Add;
        }

        private void OnDisable()
        {
            FurniturePlace.FurnitureInPlaced -= Add;
        }

        private void Add(int count) => _progressBar.Add(count);
    }
}