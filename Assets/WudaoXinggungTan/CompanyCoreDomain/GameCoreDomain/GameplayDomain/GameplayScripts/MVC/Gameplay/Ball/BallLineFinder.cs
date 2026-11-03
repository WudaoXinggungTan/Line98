using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameplayScripts.MVC.Gameplay.Board;

namespace GameplayScripts.MVC.Gameplay.Ball
{
    // Line98 scoring rule: MIN_LINE_LENGTH or more balls of the same colour in a straight line
    // (horizontal, vertical or diagonal). Only full balls count; previews (negative values) break a line.
    public static class BallLineFinder
    {
        #region Constants

        public const int MIN_LINE_LENGTH = 5;

        #endregion

        #region Dependencies

        // Each axis is walked both ways from the centre cell
        private static readonly Vector2Int[] Axes =
        {
            new Vector2Int(1, 0),  // horizontal
            new Vector2Int(0, 1),  // vertical
            new Vector2Int(1, 1),  // diagonal
            new Vector2Int(1, -1)  // other diagonal
        };

        #endregion

        #region Public Methods

        // Every ball in a long-enough line through 'center', centre included; empty when there is no line
        public static List<Vector2Int> FindLinesThrough(BoardModel board, Vector2Int center)
        {
            var cellsInLines = new HashSet<Vector2Int>();
            if (!board.IsInside(center))
            {
                return cellsInLines.ToList();
            }

            var color = board.Get(center);
            if (color <= 0)
            {
                return cellsInLines.ToList();
            }

            foreach (var axis in Axes)
            {
                var line = new List<Vector2Int> { center };
                CollectSameColor(board, center, axis, color, line);
                CollectSameColor(board, center, -axis, color, line);

                if (line.Count >= MIN_LINE_LENGTH)
                {
                    cellsInLines.UnionWith(line);
                }
            }

            return cellsInLines.ToList();
        }

        #endregion

        #region Private Methods

        private static void CollectSameColor(BoardModel board, Vector2Int start, Vector2Int step, int color, List<Vector2Int> line)
        {
            for (var cell = start + step; board.IsInside(cell) && board.Get(cell) == color; cell += step)
            {
                line.Add(cell);
            }
        }

        #endregion
    }
}
