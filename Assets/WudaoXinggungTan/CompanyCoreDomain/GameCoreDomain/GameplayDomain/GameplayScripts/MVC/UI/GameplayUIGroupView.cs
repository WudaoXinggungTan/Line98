using System;
using UnityEngine;
using CompanyCoreScripts.MVC.UISystem;
using GameplayScripts.MVC.UI.HUD;

namespace GameplayScripts.MVC.UI
{
    public class GameplayUIGroupView : BaseUIGroupView
    {
        #region Canvas Views

        [field: SerializeField] public HudUICanvasView HudUICanvasView { get; private set; }
        
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