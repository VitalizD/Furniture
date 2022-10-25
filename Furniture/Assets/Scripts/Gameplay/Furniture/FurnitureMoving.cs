using UnityEngine;
using Service;

namespace Gameplay.Furniture
{
    public class FurnitureMoving : MonoBehaviour
    {
        [SerializeField] private float _force;

        private HingeJoint2D _hingeJoint2D;
        private Camera _mainCamera;

        private Rigidbody2D _movingPoint;

        private void Awake()
        {
            _hingeJoint2D = GetComponent<HingeJoint2D>();
            _mainCamera = Camera.main;
        }

        private void OnMouseDown()
        {
            var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _movingPoint = Instantiate(GameStorage.Instanse.MovingCenter, mousePosition, Quaternion.identity)
                .GetComponent<Rigidbody2D>();
            _hingeJoint2D.connectedBody = _movingPoint;
            _hingeJoint2D.anchor = transform.InverseTransformPoint(_movingPoint.transform.position);
        }

        private void OnMouseDrag()
        {
            if (_movingPoint == null)
                return;

            var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var followPosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
            _movingPoint.AddForce((followPosition - _movingPoint.transform.position) * _force);
        }

        private void OnMouseUp()
        {
            if (_movingPoint == null)
                return;

            Destroy(_movingPoint.gameObject);
            _hingeJoint2D.anchor = Vector2.zero;
        }
    }
}
