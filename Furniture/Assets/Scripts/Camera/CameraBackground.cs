using UnityEngine;

namespace CameraEngine
{
    [RequireComponent(typeof(Camera))]
    public class CameraBackground : MonoBehaviour
    {
        [SerializeField] private Color[] _possibleColors;

        private Camera _camera;

        public void SetPreparedColor() => SetColor(_possibleColors[Random.Range(0, _possibleColors.Length)]);

        public void SetColor(Color color) => _camera.backgroundColor = color;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }
    }
}
