using UnityEngine;
using UnityEngine.UI;
using CompanyCoreScripts.MVC.UISystem;

namespace GameplayScripts.MVC.UI.HUD
{
    public class HudUICanvasView : BaseUICanvasView
    {
        #region Dependencies

        private Canvas screenCanvas;

        #endregion

        #region Public Methods

        public void InitEntryPoint(Camera uiCamera)
        {
            base.InitEntryPoint();
            screenCanvas = GetComponent<Canvas>();
            screenCanvas.worldCamera = uiCamera;
        }

        public void StartEntryPoint()
        {
        }

        public void InitExitPoint()
        {
        }

        #endregion
    }
}