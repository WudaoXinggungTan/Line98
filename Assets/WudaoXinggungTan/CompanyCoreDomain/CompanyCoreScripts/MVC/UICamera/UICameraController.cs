using UnityEngine;

namespace CompanyCoreScripts.MVC.UICamera
{
    public class UICameraController : IUICameraController
    {
        #region Dependencies

        private readonly UICameraView uiCameraView;

        #endregion

        #region Properties

        public Camera UICamera => uiCameraView.Camera;

        #endregion

        #region Constructor

        public UICameraController(UICameraView uiCameraView)
        {
            this.uiCameraView = uiCameraView;
        }

        #endregion
    }
}