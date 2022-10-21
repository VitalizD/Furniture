using UnityEngine;
using Service;

[RequireComponent(typeof(Rigidbody2D))]
public class FurnitureMoving : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _velocityForDamage;

    private Rigidbody2D _rigitBody2D;
    private Camera _mainCamera;

    private bool _holdDown = false;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _rigitBody2D = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        _holdDown = true;
    }

    private void OnMouseUp()
    {
        _holdDown = false;
    }

    private void OnMouseDrag()
    {
        var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var followPosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
        _rigitBody2D.AddForce((followPosition - transform.position) * _force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_holdDown)
            return;
        
        if (_rigitBody2D.velocity.magnitude >= _velocityForDamage)
        {
            var spawnPoint = new Vector3(collision.transform.position.x, collision.transform.position.y, -10f);
            Instantiate(GameStorage.Instanse.Bum, spawnPoint, GameStorage.Instanse.Bum.transform.localRotation);
        }
    }
}
