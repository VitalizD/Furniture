using UnityEngine;

namespace Service
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteManager : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sprites;

        private SpriteRenderer _spriteRenderer;

        public void SetRandomSprite()
        {
            if (_sprites == null || _sprites.Length == 0)
                return;
            _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}