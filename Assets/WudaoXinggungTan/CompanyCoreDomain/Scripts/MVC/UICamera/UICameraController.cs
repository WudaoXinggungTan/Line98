using UnityEngine;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.MVC.UICamera
{
    public class UICameraController : IUICameraController
    {
        private readonly UICameraView _uiCameraView;
        
        public Camera UICamera => _uiCameraView.Camera;

        public UICameraController(UICameraView uiCameraView)
        {
            _uiCameraView = uiCameraView;
        }
    }
}
