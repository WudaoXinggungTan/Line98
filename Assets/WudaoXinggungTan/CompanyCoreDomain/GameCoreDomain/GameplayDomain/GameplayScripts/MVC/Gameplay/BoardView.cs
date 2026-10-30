using UnityEngine;
using UnityEngine.UI;

namespace GameplayScripts.MVC.Gameplay
{
    /*Responsible for:

    - Put visual ball at cell
    - Move visual ball
    - Remove visual ball
    - Animate*/

    [ExecuteInEditMode]
    [RequireComponent(typeof(GridLayoutGroup))]
    public class BoardView : MonoBehaviour
    {
        private GridLayoutGroup gridLayout;
        private RectTransform rectTransform;

        [Header("Grid Dimensions")]
        public int columns = 8; // Set this to match your 8x8 grid
        public int rows = 8;

        void Awake()
        {
            gridLayout = GetComponent<GridLayoutGroup>();
            rectTransform = GetComponent<RectTransform>();
        }

        void OnRectTransformDimensionsChange()
        {
            UpdateGrid();
        }

        // This ensures it updates immediately in the Inspector editor
        void OnValidate()
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            if (gridLayout == null || rectTransform == null)
                return;

            // Calculate cell width based on the grid's CURRENT actual width
            float totalSpacingX = gridLayout.padding.left + gridLayout.padding.right + (gridLayout.spacing.x * (columns - 1));
            float cellWidth = (rectTransform.rect.width - totalSpacingX) / columns;

            // Calculate cell height based on the grid's CURRENT actual height
            float totalSpacingY = gridLayout.padding.top + gridLayout.padding.bottom + (gridLayout.spacing.y * (rows - 1));
            float cellHeight = (rectTransform.rect.height - totalSpacingY) / rows;

            // Use the smaller of the two so cells always fit inside the shrunk container
            float finalSize = Mathf.Min(cellWidth, cellHeight);

            // This physically overwrites the "Cell Size" property in real-time
            if (finalSize > 0)
            {
                gridLayout.cellSize = new Vector2(finalSize, finalSize);
            }
        }
    }
}