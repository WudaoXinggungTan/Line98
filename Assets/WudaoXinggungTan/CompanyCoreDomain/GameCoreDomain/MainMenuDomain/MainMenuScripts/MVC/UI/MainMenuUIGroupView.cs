using UnityEngine;
using CompanyCoreScripts.MVC.UISystem;
using MainMenuScripts.MVC.UI.MainCanvas;
using MainMenuScripts.MVC.UI.SettingsCanvas;

namespace MainMenuScripts.MVC.UI
{
    public class MainMenuUIGroupView : BaseUIGroupView
    {
        #region Canvas Views

        [field: SerializeField] public MainUICanvasView MainUICanvasView { get; private set; }
        [field: SerializeField] public SettingsUICanvasView SettingsUICanvasView { get; private set; }

        #endregion

        #region Public Methods

        public override void InitEntryPoint()
        {
            base.InitEntryPoint();
        }

        public override void StartEntryPoint()
        {
            base.StartEntryPoint();
        }

        public override void InitExitPoint()
        {
            base.InitExitPoint();
        }

        #endregion
    }
}