using UnityEngine;

namespace Service
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private bool _counterclockwise;

        private void Update()
        {
            var rotationVector = new Vector3(0, 0, _speed * Time.deltaTime);
            transform.eulerAngles += _counterclockwise ? rotationVector : -rotationVector;
        }
    }
}