using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayScripts.MVC.Gameplay.Board
{
    // Board data only, no Unity objects. Cell values: 0 = empty (later: +color = big ball, -color = small ball)
    // Coordinates are Vector2Int with x = column and y = row, (0, 0) is the top-left cell
    public class BoardModel
    {
        #region Dependencies

        private readonly int[,] cells;

        #endregion

        #region Properties

        public int Rows { get; }
        public int Columns { get; }

        #endregion

        #region Constructor

        public BoardModel(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            cells = new int[rows, columns];
        }

        #endregion

        #region Public Methods

        public bool IsInside(Vector2Int cell)
        {
            return cell.x >= 0 && cell.x < Columns && cell.y >= 0 && cell.y < Rows;
        }

        public int Get(Vector2Int cell)
        {
            return cells[cell.y, cell.x];
        }

        public void Set(Vector2Int cell, int value)
        {
            cells[cell.y, cell.x] = value;
        }

        public List<Vector2Int> GetEmptyCells()
        {
            var emptyCells = new List<Vector2Int>();
            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    if (cells[row, column] == 0)
                    {
                        emptyCells.Add(new Vector2Int(column, row));
                    }
                }
            }

            return emptyCells;
        }

        public void Clear()
        {
            Array.Clear(cells, 0, cells.Length);
        }

        #endregion
    }
}
