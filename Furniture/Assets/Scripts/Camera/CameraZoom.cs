using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Camera))]
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float _zoomMin;
        [SerializeField] private float _zoomMax;
        [SerializeField] private float _zoomSpeedTouch;
        [SerializeField] private float _zoomSpeedWheel;

        private Camera _camera;

        private float _initialZoom;

        public void SetZoom(float value) => _camera.orthographicSize = Mathf.Clamp(value, _zoomMin, _zoomMax);

        public void SetMinZoom() => _camera.orthographicSize = _zoomMin;

        public void SetMaxZoom() => _camera.orthographicSize = _zoomMax;

        public void SetInitialZoom() => _camera.orthographicSize = _initialZoom;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _initialZoom = _camera.orthographicSize;
        }

        private void Update()
        {
            if (Input.touchCount == 2)
            {
                var first = Input.GetTouch(0);
                var second = Input.GetTouch(1);

                var firstPosition = first.position - first.deltaPosition;
                var secondPosition = second.position - second.deltaPosition;

                var firstlyTouchDisctance = Vector2.Distance(firstPosition, secondPosition);
                var currentTouchDisctance = Vector2.Distance(first.position, second.position);

                var delta = currentTouchDisctance - firstlyTouchDisctance;
                Zoom(delta * _zoomSpeedTouch * Time.deltaTime);
            }
            Zoom(Input.GetAxis("Mouse ScrollWheel") * _zoomSpeedWheel * Time.deltaTime);
        }

        private void Zoom(float delta)
        {
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - delta, _zoomMin, _zoomMax);
        }
    }
}