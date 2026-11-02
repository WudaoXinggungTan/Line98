using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameplayScripts.MVC.Gameplay.Board
{
    // Lives on BoardArea, a RectTransform stretched over the free space below the top buttons.
    // Cells are created inside cellsContainer (BoardGrid) and sized so the square board is as wide as the screen's
    // safe area, while staying inside the area's height.
    [RequireComponent(typeof(RectTransform))]
    public class BoardView : MonoBehaviour, IPointerClickHandler
    {
        #region Dependencies

        [SerializeField] private int rows = 9;
        [SerializeField] private int columns = 9;
        [SerializeField] private RectTransform cellsContainer;
        [SerializeField] private Image cellPrefab;

        private readonly List<Image> cells = new();
        private Action<Vector2Int> onCellClicked;

        #endregion

        #region Properties

        public int Rows => rows;
        public int Columns => columns;

        #endregion

        #region Unity Methods

        // Called whenever BoardArea is resized (screen size, rotation, canvas scaler update)
        private void OnRectTransformDimensionsChange()
        {
            LayoutCells();
        }

        #endregion

        #region Public Methods

        public void BuildCells(Action<Vector2Int> onCellClicked)
        {
            ClearCells();
            this.onCellClicked = onCellClicked;

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    var cell = Instantiate(cellPrefab, cellsContainer);
                    cell.name = $"Cell_{row}_{column}";
                    cell.raycastTarget = true; // clicks on a cell bubble up to OnPointerClick

                    // Anchor every cell to the board's top-left corner, so its position is a simple multiple of the cell size
                    var cellRect = cell.rectTransform;
                    cellRect.anchorMin = new Vector2(0f, 1f);
                    cellRect.anchorMax = new Vector2(0f, 1f);
                    cellRect.pivot = new Vector2(0f, 1f);

                    cells.Add(cell);
                }
            }

            LayoutCells();
        }

        public void ClearCells()
        {
            foreach (var cell in cells)
            {
                if (cell != null)
                {
                    Destroy(cell.gameObject);
                }
            }

            cells.Clear();
        }

        public RectTransform GetCell(Vector2Int cell)
        {
            return cells[cell.y * columns + cell.x].rectTransform;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var index = cells.FindIndex(cell => cell.gameObject == eventData.pointerPressRaycast.gameObject);
            if (index < 0)
            {
                return;
            }

            onCellClicked?.Invoke(new Vector2Int(index % columns, index / columns));
        }

        #endregion

        #region Private Methods

        private void LayoutCells()
        {
            var area = ((RectTransform)transform).rect;
            if (cells.Count == 0 || area.width <= 0f || area.height <= 0f)
            {
                return;
            }

            // Width reaches the edges of the screen's safe area, height stays below the top buttons
            var safeWidth = GetSafeAreaWidth(out var safeCenterOffsetX);
            var width = Mathf.Min(area.width, safeWidth);

            // Biggest whole-unit cell that still fits in both directions
            var cellSize = Mathf.Floor(Mathf.Min(width / columns, area.height / rows));
            cellsContainer.sizeDelta = new Vector2(columns * cellSize, rows * cellSize);
            cellsContainer.anchoredPosition = new Vector2(safeCenterOffsetX, 0f);

            for (var i = 0; i < cells.Count; i++)
            {
                var row = i / columns;
                var column = i % columns;
                var cellRect = cells[i].rectTransform;
                cellRect.sizeDelta = new Vector2(cellSize, cellSize);
                cellRect.anchoredPosition = new Vector2(column * cellSize, -row * cellSize);
            }
        }

        // Screen.safeArea is in pixels; dividing by the canvas scale factor converts it to canvas units
        private float GetSafeAreaWidth(out float centerOffsetX)
        {
            var scaleFactor = GetComponentInParent<Canvas>().rootCanvas.scaleFactor;
            if (scaleFactor <= 0f)
            {
                centerOffsetX = 0f;
                return float.MaxValue;
            }

            var safeArea = Screen.safeArea;
            centerOffsetX = (safeArea.center.x - Screen.width / 2f) / scaleFactor;
            return safeArea.width / scaleFactor;
        }

        #endregion
    }
}
