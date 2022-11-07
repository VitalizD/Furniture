using UnityEngine;
using Service;

namespace Gameplay.Furniture
{
    public class FurnitureMoving : MonoBehaviour
    {
        [SerializeField] private float _force;
        [SerializeField] private float _velocityForDamage;

        private Rigidbody2D _rigidbody2D;
        private HingeJoint2D _hingeJoint2D;
        private Camera _mainCamera;
        private SpriteRenderer _spriteRenderer;

        private Rigidbody2D _movingPoint = null;
        private CapturePoint _currentCapturePoint = null;
        private Color _initColor;
        private float _fixedZPosition;
        private float _initWeight;
        private bool _locked = false;

        public int PlaceHash { get; set; }

        public void SetSameWeight() => _rigidbody2D.mass = 1f;

        public void ReturnWeight() => _rigidbody2D.mass = _initWeight;

        public void SetForce(float value) => _force = value;

        public void SetColor(Color color) => _spriteRenderer.color = color;

        public void SetInitColor() => _spriteRenderer.color = _initColor;

        public void SetLock(bool value)
        {
            _locked = value;
            SetActiveCapturePoints(!value);
        }

        public void SetCapturePoint(Vector2 point)
        {
            if (_locked)
                return;

            _hingeJoint2D.enabled = true;
            _movingPoint = Instantiate(GameStorage.Instanse.MovingCenter, point, Quaternion.identity)
                .GetComponent<Rigidbody2D>();
            _hingeJoint2D.connectedBody = _movingPoint;
            _hingeJoint2D.anchor = transform.InverseTransformPoint(_movingPoint.transform.position);
        }

        public void RemoveCapturePoint()
        {
            if (_movingPoint == null || _locked)
                return;

            Destroy(_movingPoint.gameObject);
            _hingeJoint2D.anchor = Vector2.zero;
            _hingeJoint2D.enabled = false;
        }

        public void Drag(Vector2 toPoint)
        {
            if (_movingPoint == null || _locked)
                return;

            var followPosition = new Vector3(toPoint.x, toPoint.y, 0f);
            _movingPoint.AddForce((followPosition - _movingPoint.transform.position) * _force * Time.fixedDeltaTime);
        }

        public void SetActiveCapturePoints(bool value)
        {
            _currentCapturePoint = null;
            foreach (Transform child in transform)
                child.gameObject.SetActive(value);
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _hingeJoint2D = GetComponent<HingeJoint2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _hingeJoint2D.enabled = false;
            _mainCamera = Camera.main;
            _initColor = _spriteRenderer.color;
            _fixedZPosition = transform.position.z;
            _initWeight = _rigidbody2D.mass;

            SetSameScaleToCapturePoints();
        }

        private void Update()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _fixedZPosition);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_movingPoint == null)
                return;

            if (_rigidbody2D.velocity.magnitude * _rigidbody2D.mass >= _velocityForDamage)
            {
                var spawnPoint = new Vector3(collision.transform.position.x, collision.transform.position.y, -10f);
                Instantiate(GameStorage.Instanse.Bum, spawnPoint, GameStorage.Instanse.Bum.transform.localRotation);
            }
        }

        private void OnMouseDown()
        {
            var capturePoint = GetCapturePoint();
            if (capturePoint != null)
            {
                _currentCapturePoint = capturePoint;
                capturePoint.SendMessage("OnMouseDown");
            }
        }

        private void OnMouseDrag()
        {
            if (_currentCapturePoint == null)
                return;

            _currentCapturePoint.SendMessage("OnMouseDrag");
        }

        private void OnMouseUp()
        {
            if (_currentCapturePoint == null)
                return;

            _currentCapturePoint.SendMessage("OnMouseUp");
            _currentCapturePoint = null;
        }

        private CapturePoint GetCapturePoint()
        {
            var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (Physics2D.Raycast(mousePosition, Vector2.zero))
                return hit.collider.GetComponent<CapturePoint>();
            return null;
        }

        private void SetSameScaleToCapturePoints()
        {
            var capturePoints = transform.GetComponentsInChildren<CapturePoint>();
            var capturePointScale = GameStorage.Instanse.CapturePointScale;
            foreach (var capturePoint in capturePoints)
            {
                capturePoint.transform.localScale = Vector3.one;
                capturePoint.transform.localScale = new Vector3(capturePointScale / capturePoint.transform.lossyScale.x,
                    capturePointScale / capturePoint.transform.lossyScale.y, capturePointScale / capturePoint.transform.lossyScale.z);
            }
        }
    }
}
