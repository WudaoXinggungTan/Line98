using System;
using UnityEngine;

namespace GameplayScripts.MVC.Gameplay.Board
{
    public class BoardController : IBoardController
    {
        #region Dependencies

        private readonly BoardView boardView;

        #endregion

        #region Properties

        public BoardModel BoardModel { get; private set; }

        public event Action<Vector2Int> CellClicked;

        #endregion

        #region Constructor

        public BoardController(BoardView boardView)
        {
            this.boardView = boardView;
        }

        #endregion

        #region Public Methods

        public void InitEntryPoint()
        {
            BoardModel = new BoardModel(boardView.Rows, boardView.Columns);
            boardView.BuildCells(OnCellClicked);
        }

        public void StartEntryPoint()
        {
        }

        public void InitExitPoint()
        {
            boardView.ClearCells();
        }

        public RectTransform GetCellTransform(Vector2Int cell)
        {
            return boardView.GetCell(cell);
        }

        #endregion

        #region Private Methods

        private void OnCellClicked(Vector2Int cell)
        {
            CellClicked?.Invoke(cell);
        }

        #endregion
    }
}
