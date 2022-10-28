using UnityEngine;

namespace Gameplay.Furniture
{
    public class FurniturePlace2 : MonoBehaviour
    {
        private const float _moveOnPlaceSpeed = 5f;

        [SerializeField] private float _requiredDistance;
        [SerializeField] private float _requiredAngleSpread;
        [SerializeField] private Color _inPlaceHighlight;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private GameObject[] _furnitureVariants;

        private bool inPlace = false;
        //private Color _initColor;
        private FurnitureMoving _target;

        private void Awake()
        {
            if (_furnitureVariants.Length == 0)
                return;

            var randomFurniture = _furnitureVariants[Random.Range(0, _furnitureVariants.Length)];
            _target = Instantiate(randomFurniture, Vector2.zero, Quaternion.identity).GetComponent<FurnitureMoving>();
            var targetSprite = _target.GetComponent<SpriteRenderer>();
            _sprite.sprite = targetSprite.sprite;
            //_initColor = targetSprite.color;
            transform.localScale = _target.transform.localScale;
        }

        private void Update()
        {
            if (_target == null)
                return;

            var minAngle = transform.rotation.z - _requiredAngleSpread;
            var maxAngle = transform.rotation.z + _requiredAngleSpread;
            var targetAngle = _target.transform.eulerAngles.z > 180f ?
                _target.transform.eulerAngles.z - 360f : _target.transform.eulerAngles.z;

            if (!inPlace && Vector2.Distance(transform.position, _target.transform.position) <= _requiredDistance
                && targetAngle >= minAngle && targetAngle <= maxAngle)
            {
                SetInPlace(true);
            }
            else if (inPlace && (Vector2.Distance(transform.position, _target.transform.position) > _requiredDistance
                || targetAngle < minAngle || targetAngle > maxAngle))
            {
                SetInPlace(false);
            }

            if (inPlace)
            {
                _target.transform.SetPositionAndRotation(
                    Vector3.Lerp(_target.transform.position, transform.position, _moveOnPlaceSpeed * Time.deltaTime), 
                    Quaternion.Lerp(_target.transform.rotation, transform.rotation, _moveOnPlaceSpeed * Time.deltaTime));
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _requiredDistance);
        }

        private void SetInPlace(bool value)
        {
            inPlace = value;
            _target.SetLock(value);

            //if (value)
            //{
            //    _target.GetComponent<SpriteRenderer>().color = _inPlaceHighlight;
            //}
            //else
            //{
            //    _target.GetComponent<SpriteRenderer>().color = _initColor;
            //}
        }
    }
}
