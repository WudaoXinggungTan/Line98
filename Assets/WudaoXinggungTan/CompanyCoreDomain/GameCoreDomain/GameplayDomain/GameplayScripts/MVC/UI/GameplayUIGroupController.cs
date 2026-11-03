using UnityEngine;
using CompanyCoreScripts.MVC.UISystem;

namespace GameplayScripts.MVC.UI
{
    public class GameplayUIGroupController : BaseUIGroupController, IGameplayUIGroupController
    {
        #region Dependencies

        private readonly GameplayUIGroupView gameplayUIGroupView;

        #endregion

        #region Constructor

        public GameplayUIGroupController(GameplayUIGroupView gameplayUIGroupView) : base(gameplayUIGroupView)
        {
            this.gameplayUIGroupView = gameplayUIGroupView;
        }

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

        // Settings is an overlay: it opens on top of the HUD instead of replacing it (SwitchCanvas would close the HUD and the board)
        public void OpenSettingsCanvas()
        {
            gameplayUIGroupView.SettingsUICanvasView.StartCanvas();
        }

        public void CloseSettingsCanvas()
        {
            gameplayUIGroupView.SettingsUICanvasView.CloseCanvas();
        }

        public void OpenHowToPlayCanvas()
        {
            
        }

        #endregion
    }
}