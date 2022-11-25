using UnityEngine;
using System;
using System.Collections;

namespace Gameplay.Furniture
{
    public class FurniturePlace : MonoBehaviour
    {
        private const float _moveOnPlaceSpeed = 5f;

        [SerializeField] private bool considerAngle = true;
        [SerializeField] private float _requiredDistance;
        [SerializeField] private float _requiredAngleSpread;
        [SerializeField] private float _highlightTime;
        [SerializeField] private Color _inPlaceTemporaryHighlight;
        [SerializeField] private Color _inPlaceHighlight;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private FurnitureMoving[] _targets;

        private Coroutine _highlightCoroutine;

        private bool busy = false;
        private int hash;

        public static event Action<int> FurnitureInPlaced;

        private void Awake()
        {
            if (_targets.Length == 0)
                return;

            var targetSprite = _targets[0].GetComponent<SpriteRenderer>();
            _sprite.sprite = targetSprite.sprite;
            transform.localScale = _targets[0].transform.localScale;
            hash = GetHashCode();
        }

        private void Update()
        {
            if (_targets.Length == 0)
                return;

            for (var i = 0; i < _targets.Length; ++i)
            {
                var target = _targets[i];
                var minAngle = transform.eulerAngles.z - _requiredAngleSpread;
                var maxAngle = transform.eulerAngles.z + _requiredAngleSpread;
                //var targetAngle = target.transform.eulerAngles.z < -15f ?
                //    (target.transform.eulerAngles.z % 360f) + 360f : target.transform.eulerAngles.z % 360f;
                var targetAngle = target.transform.rotation.eulerAngles.z;

                if (target.PlaceHash != hash && !busy && Vector2.Distance(transform.position, target.transform.position) <= _requiredDistance
                    && (!considerAngle || (targetAngle >= minAngle && targetAngle <= maxAngle)))
                {
                    SetInPlace(target, true);
                }
                else if (target.PlaceHash == hash && busy && (Vector2.Distance(transform.position, target.transform.position) > _requiredDistance
                    || (considerAngle && (targetAngle < minAngle || targetAngle > maxAngle))))
                {
                    SetInPlace(target, false);
                }

                if (target.PlaceHash == hash && busy)
                {
                    target.transform.SetPositionAndRotation(
                        Vector3.Lerp(target.transform.position, transform.position, _moveOnPlaceSpeed * Time.deltaTime),
                        Quaternion.Lerp(target.transform.rotation, transform.rotation, _moveOnPlaceSpeed * Time.deltaTime));
                    break;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _requiredDistance);
        }

        private void SetInPlace(FurnitureMoving furniture, bool value)
        {
            busy = value;
            furniture.SetLock(value);
            FurnitureInPlaced?.Invoke(value ? 1 : -1);
            if (value)
            {
                _highlightCoroutine = StartCoroutine(Highlight(furniture));
                furniture.PlaceHash = hash;
            }
            else
            {
                if (_highlightCoroutine != null)
                    StopCoroutine(_highlightCoroutine);
                furniture.PlaceHash = 0;
                furniture.SetInitColor();
            }
        }

        private IEnumerator Highlight(FurnitureMoving furniture)
        {
            furniture.SetColor(_inPlaceTemporaryHighlight);
            yield return new WaitForSeconds(_highlightTime);
            furniture.SetColor(_inPlaceHighlight);
        }
    }
}
