using System;
using UnityEngine;

namespace GameplayScripts.MVC.Gameplay.Board
{
    public interface IBoardController
    {
        public BoardModel BoardModel { get; }
        public event Action<Vector2Int> CellClicked;

        public void InitEntryPoint();
        public void StartEntryPoint();
        public void InitExitPoint();

        public RectTransform GetCellTransform(Vector2Int cell);
    }
}
