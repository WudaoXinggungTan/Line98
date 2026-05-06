using UnityEngine;

namespace CompanyCoreScripts.MVC.UISystem
{
    public class BaseUIGroupController : IBaseUIGroupController
    {
        #region Dependencies

        public BaseUIGroupView BaseUIGroupView { get; }

        #endregion

        #region Constructor

        public BaseUIGroupController(BaseUIGroupView baseUIGroupView)
        {
            BaseUIGroupView = baseUIGroupView;
        }

        #endregion

        #region Virtual Methods

        public virtual void InitEntryPoint()
        {
            BaseUIGroupView.InitEntryPoint();
        }
        
        public virtual void StartEntryPoint()
        {
            BaseUIGroupView.StartEntryPoint();
        }

        public virtual void InitExitPoint()
        {
            BaseUIGroupView.InitExitPoint();
        }

        #endregion
    }
}