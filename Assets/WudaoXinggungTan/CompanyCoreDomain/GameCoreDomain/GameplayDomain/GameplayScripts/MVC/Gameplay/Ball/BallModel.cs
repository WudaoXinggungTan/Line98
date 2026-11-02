using UnityEngine;

namespace GameplayScripts.MVC.Gameplay.Ball
{
    public class BallModel
    {
        #region Properties

        public BallColorType Color { get; }
        public Vector2Int Cell { get; }

        #endregion

        #region Constructor

        public BallModel(BallColorType color, Vector2Int cell)
        {
            Color = color;
            Cell = cell;
        }

        #endregion
    }
}
