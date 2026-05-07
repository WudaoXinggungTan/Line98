using System;
using UnityEngine;

namespace CompanyCoreScripts.MVC.UISystem
{
    public class BaseUIGroupView : MonoBehaviour
    {
        #region Dependencies

        [Header("Main Properties")]
        public BaseUICanvasView startCanvasView;

        private BaseUICanvasView[] canvasViewArray = new BaseUICanvasView[0];

        private BaseUICanvasView previousCanvasView;
        public BaseUICanvasView PreviousCanvasView { get { return previousCanvasView; } }

        private BaseUICanvasView currentCanvasView;
        public BaseUICanvasView CurrentCanvasView { get { return currentCanvasView; } }

        #endregion

        #region Actions

        private Action onCanvasSwitched;

        #endregion

        #region Virtual Methods

        public virtual void InitEntryPoint()
        {
            canvasViewArray = GetComponentsInChildren<BaseUICanvasView>(true);
            DisableAllCanvas();
        }

        public virtual void StartEntryPoint()
        {
            if (startCanvasView)
            {
                SwitchCanvas(startCanvasView);
            }
        }
        
        public virtual void InitExitPoint()
        {
        }

        #endregion

        #region Public Methods

        public void SwitchCanvas(BaseUICanvasView canvasView)
        {
            if (canvasView)
            {
                if (currentCanvasView)
                {
                    currentCanvasView.CloseCanvas();
                    previousCanvasView = currentCanvasView;
                }

                currentCanvasView = canvasView;
                currentCanvasView.StartCanvas();

                OnCanvasSwitched();
            }
        }
        
        public void SwitchToPreviousCanvas()
        {
            if (previousCanvasView)
            {
                SwitchCanvas(previousCanvasView);
            }
        }

        #endregion

        #region Private Methods

        private void OnCanvasSwitched()
        {
            onCanvasSwitched?.Invoke();
        }

        private void EnableAllCanvas()
        {
            foreach (BaseUICanvasView canvasElement in canvasViewArray)
            {
                canvasElement.enabled = true;
            }
        }

        private void DisableAllCanvas()
        {
            foreach (var canvasElement in canvasViewArray)
            {
                canvasElement.enabled = false;
            }
        }

        #endregion
    }
}