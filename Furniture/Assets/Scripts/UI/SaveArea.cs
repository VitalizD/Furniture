using UnityEngine;

namespace UI
{
    public class SaveArea : MonoBehaviour
    {
        public void Refresh()
        {
            var safeArea = Screen.safeArea;
            var rectTransform = GetComponent<RectTransform>();

            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }

        private void Start()
        {
            Refresh();
        }
    }
}
