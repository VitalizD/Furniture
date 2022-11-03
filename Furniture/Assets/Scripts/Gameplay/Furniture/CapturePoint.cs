using UnityEngine;

namespace Gameplay.Furniture
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CapturePoint : MonoBehaviour
    {
        private Camera _mainCamera;
        private FurnitureMoving _furniture;
        private SpriteRenderer _spriteRenderer;

        private float _initOpacity;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _furniture = transform.parent.GetComponent<FurnitureMoving>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _initOpacity = _spriteRenderer.color.a;
        }

        private void OnEnable()
        {
            SetOpacity(_initOpacity);
        }

        private void OnMouseDown()
        {
            _furniture.SetCapturePoint(GetMousePosition());
            SetOpacity(1f);
        }

        private void OnMouseDrag()
        {
            _furniture.Drag(GetMousePosition());
        }

        private void OnMouseUp()
        {
            _furniture.RemoveCapturePoint();
            SetOpacity(_initOpacity);
        }

        private void SetOpacity(float value) => _spriteRenderer.color = new Color(_spriteRenderer.color.r, 
            _spriteRenderer.color.g, _spriteRenderer.color.b, value);

        private Vector2 GetMousePosition() => _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
