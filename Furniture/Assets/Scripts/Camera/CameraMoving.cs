using UnityEngine;

namespace CameraEngine
{
    [RequireComponent(typeof(CameraBounds))]
    public class CameraMoving : MonoBehaviour
    {
        [SerializeField] private float _sharpness;

        private Camera _camera;
        private CameraBounds _bounds;

        private Vector3 _startPosition;
        private Vector3 _toPosition;

        public bool Locked { get; set; } = false;

        public void ResetPosition()
        {
            transform.position = new Vector3(0, 0, transform.position.z);
            _startPosition = Vector3.zero;
            _toPosition = transform.position;
        }

        private void Awake()
        {
            _bounds = GetComponent<CameraBounds>();
            _camera = GetComponent<Camera>();
            _toPosition = transform.position;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
                Locked = false;

            if (Locked || Input.touchCount > 1)
            {
                ResetStartPosition();
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(0))
            {
                var delta = _startPosition - _camera.ScreenToWorldPoint(Input.mousePosition);
                _toPosition = new Vector3(
                    Mathf.Clamp(transform.position.x + delta.x, _bounds.LeftLimit, _bounds.RightLimit),
                    Mathf.Clamp(transform.position.y + delta.y, _bounds.BottomLimit, _bounds.TopLimit),
                    transform.position.z);
            }
            transform.position = Vector3.Lerp(transform.position, _toPosition, _sharpness * Time.deltaTime);
        }

        private void ResetStartPosition() => _startPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}