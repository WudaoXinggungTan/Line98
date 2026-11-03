using UnityEngine;

namespace CompanyCoreScripts.Helpers.UIHelpers
{
    // Fits this RectTransform to the screen's safe area (no notch, camera cut-out or home indicator inside it).
    // Put it on a direct child of a full-screen canvas and place the UI that must stay visible inside it.
    // Runs only at runtime, so the scene keeps a plain full-screen rect while editing.
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaFitter : MonoBehaviour
    {
        #region Dependencies

        private Rect appliedSafeArea;
        private Vector2Int appliedScreenSize;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            ApplySafeArea();
        }

        // The safe area changes on rotation and when switching devices in the Device Simulator
        private void Update()
        {
            if (Screen.safeArea != appliedSafeArea || Screen.width != appliedScreenSize.x || Screen.height != appliedScreenSize.y)
            {
                ApplySafeArea();
            }
        }

        #endregion

        #region Private Methods

        private void ApplySafeArea()
        {
            if (Screen.width <= 0 || Screen.height <= 0)
            {
                return;
            }

            var safeArea = Screen.safeArea;
            appliedSafeArea = safeArea;
            appliedScreenSize = new Vector2Int(Screen.width, Screen.height);

            // Safe area is in screen pixels; anchors are fractions of the full-screen parent
            var rect = (RectTransform)transform;
            rect.anchorMin = new Vector2(safeArea.xMin / Screen.width, safeArea.yMin / Screen.height);
            rect.anchorMax = new Vector2(safeArea.xMax / Screen.width, safeArea.yMax / Screen.height);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }

        #endregion
    }
}
