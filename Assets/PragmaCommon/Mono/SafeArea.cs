using UnityEngine;

namespace Pragma.Common
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeArea : MonoBehaviour
    {
        private void Awake()
        {
            var rectTransform = GetComponent<RectTransform>();
            var anchorMin = Screen.safeArea.position;
            var anchorMax = Screen.safeArea.position + Screen.safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }
}