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

        private Rigidbody2D _movingPoint;
        private bool _locked = false;

        public void SetLock(bool value)
        {
            _locked = value;
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _hingeJoint2D = GetComponent<HingeJoint2D>();
            _mainCamera = Camera.main;
        }

        private void OnMouseDown()
        {
            if (_locked)
                return;

            var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _movingPoint = Instantiate(GameStorage.Instanse.MovingCenter, mousePosition, Quaternion.identity)
                .GetComponent<Rigidbody2D>();
            _hingeJoint2D.connectedBody = _movingPoint;
            _hingeJoint2D.anchor = transform.InverseTransformPoint(_movingPoint.transform.position);
        }

        private void OnMouseDrag()
        {
            if (_movingPoint == null || _locked)
                return;

            var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var followPosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
            _movingPoint.AddForce((followPosition - _movingPoint.transform.position) * _force);
        }

        private void OnMouseUp()
        {
            if (_movingPoint == null || _locked)
                return;

            Destroy(_movingPoint.gameObject);
            _hingeJoint2D.anchor = Vector2.zero;
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
    }
}
