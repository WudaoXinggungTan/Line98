using System.Collections.Generic;
using UnityEngine;
using GameplayScripts.MVC.Gameplay.Board;

namespace GameplayScripts.MVC.Gameplay.Ball
{
    // Line98 move rule: a ball travels only up/down/left/right through free cells.
    // Breadth-first search, so the path found is always one of the shortest.
    public static class BallPathFinder
    {
        #region Dependencies

        private static readonly Vector2Int[] Directions =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        #endregion

        #region Public Methods

        // path = the cells to step through, ending at 'to' (the start cell is not included)
        public static bool TryFindPath(BoardModel board, Vector2Int from, Vector2Int to, out List<Vector2Int> path)
        {
            path = null;
            if (from == to || !board.IsInside(from) || !board.IsInside(to) || !IsFree(board, to))
            {
                return false;
            }

            // cameFrom doubles as the visited set: every reached cell remembers the cell it was reached from
            var cameFrom = new Dictionary<Vector2Int, Vector2Int> { [from] = from };
            var queue = new Queue<Vector2Int>();
            queue.Enqueue(from);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current == to)
                {
                    path = BuildPath(cameFrom, from, to);
                    return true;
                }

                foreach (var direction in Directions)
                {
                    var next = current + direction;
                    if (!board.IsInside(next) || cameFrom.ContainsKey(next) || !IsFree(board, next))
                    {
                        continue;
                    }

                    cameFrom[next] = current;
                    queue.Enqueue(next);
                }
            }

            return false;
        }

        #endregion

        #region Private Methods

        // Empty cells (0) are free; small balls (negative values, added later) can be passed through too
        private static bool IsFree(BoardModel board, Vector2Int cell)
        {
            return board.Get(cell) <= 0;
        }

        private static List<Vector2Int> BuildPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int from, Vector2Int to)
        {
            var path = new List<Vector2Int>();
            for (var cell = to; cell != from; cell = cameFrom[cell])
            {
                path.Add(cell);
            }

            path.Reverse();
            return path;
        }

        #endregion
    }
}
