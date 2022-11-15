using Gameplay;
using Gameplay.Furniture;
using UnityEngine;

namespace EventHandlers
{
    [RequireComponent(typeof(Tutorial))]
    public class TutorialHandler : MonoBehaviour
    {
        private Tutorial _tutorial;

        private void Awake()
        {
            _tutorial = GetComponent<Tutorial>();
        }

        private void OnEnable()
        {
            FurnitureMoving.FurnitureCaptured += _tutorial.Deactivate;
        }

        private void OnDisable()
        {
            FurnitureMoving.FurnitureCaptured -= _tutorial.Deactivate;
        }
    }
}
