using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Camera))]
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float _zoomMin;
        [SerializeField] private float _zoomMax;
        [SerializeField] private float _zoomSpeed;

        private Camera _camera;

        public void SetZoom(float value) => _camera.orthographicSize = Mathf.Clamp(value, _zoomMin, _zoomMax);

        public void SetMinZoom() => _camera.orthographicSize = _zoomMin;

        public void SetMaxZoom() => _camera.orthographicSize = _zoomMax;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
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
                Zoom(delta * _zoomSpeed * Time.deltaTime);
            }
            Zoom(Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed * Time.deltaTime);
        }

        private void Zoom(float delta)
        {
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - delta, _zoomMin, _zoomMax);
        }
    }
}