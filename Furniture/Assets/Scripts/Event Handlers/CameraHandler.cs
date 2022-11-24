using CameraEngine;
using Gameplay.Furniture;
using Service;
using UnityEngine;

namespace EventHandlers
{
    [RequireComponent(typeof(CameraMoving))]
    public class CameraHandler : MonoBehaviour
    {
        private CameraMoving _cameraMoving;
        private CameraBackground _cameraBackground;

        private void Awake()
        {
            _cameraMoving = GetComponent<CameraMoving>();
            _cameraBackground = GetComponent<CameraBackground>();
        }

        private void OnEnable()
        {
            FurnitureMoving.FurnitureCaptured += OnFurnitureCaptured;
            GameManager.LevelStarted += OnLevelStarted;
            //FurnitureMoving.FurnitureReleased += OnFurnitureReleased;
            //FurniturePlace.FurnitureInPlaced += OnFurnitureReleased;
        }

        private void OnDisable()
        {
            FurnitureMoving.FurnitureCaptured -= OnFurnitureCaptured;
            GameManager.LevelStarted -= OnLevelStarted;
            //FurnitureMoving.FurnitureReleased -= OnFurnitureReleased;
            //FurniturePlace.FurnitureInPlaced -= OnFurnitureReleased;
        }

        private void OnFurnitureCaptured() => _cameraMoving.Locked = true;

        private void OnLevelStarted()
        {
            _cameraMoving.ResetPosition();
            _cameraBackground.SetPreparedColor();
        }

        //private void OnFurnitureReleased() => _cameraMoving.Locked = false;

        //private void OnFurnitureReleased(int value) => _cameraMoving.Locked = false;
    }
}