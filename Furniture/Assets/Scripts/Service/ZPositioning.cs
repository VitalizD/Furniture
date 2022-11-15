using UnityEngine;

namespace Service
{
    public class ZPositioning : MonoBehaviour
    {
        private void Update()
        {
            var position = transform.position;
            transform.position = new Vector3(position.x, position.y, position.y);
        }
    }
}
