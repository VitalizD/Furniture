using UnityEngine;

namespace Gameplay.Furniture
{
    public class FurniturePlace : MonoBehaviour
    {
        [SerializeField] private float _requiredDistance;
        [SerializeField] private Color _inPlaceHighlight;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private GameObject[] _furnitureVariants;

        private bool inPlace = false;
        private Color _initColor;
        private FurnitureBase _target;

        private void Awake()
        {
            if (_furnitureVariants.Length == 0)
                return;

            var randomFurniture = _furnitureVariants[Random.Range(0, _furnitureVariants.Length)];
            _target = Instantiate(randomFurniture, Vector2.zero, Quaternion.identity).GetComponent<FurnitureBase>();
            var targetSprite = _target.GetSpriteRenderer();
            _sprite.sprite = targetSprite.sprite;
            _initColor = targetSprite.color;
        }

        private void Update()
        {
            if (_target == null)
                return;

            if (!inPlace && Vector2.Distance(transform.position, _target.transform.position) <= _requiredDistance)
            {
                SetInPlace(true);
            }
            else if (inPlace && Vector2.Distance(transform.position, _target.transform.position) > _requiredDistance)
            {
                SetInPlace(false);
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

            if (value)
            {
                _target.GetSpriteRenderer().color = _inPlaceHighlight;
            }
            else
            {
                _target.GetSpriteRenderer().color = _initColor;
            }
        }
    }
}