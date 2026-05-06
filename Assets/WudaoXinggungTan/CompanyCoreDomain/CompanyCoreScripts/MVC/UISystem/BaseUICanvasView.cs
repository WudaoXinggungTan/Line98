using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace CompanyCoreScripts.MVC.UISystem
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BaseUICanvasView : MonoBehaviour
    {
        #region Dependencies

        private const string SHOW_ANIMATION_STRING = "ShowCanvas";
        private const string HIDE_ANIMATION_STRING = "HideCanvas";
        
        [SerializeField] private Selectable startSelectable;

        #endregion

        #region Actions

        private Action onCanvasStart;
        private Action onCanvasClose;

        #endregion

        #region Virtual Methods

        protected virtual void InitEntryPoint()
        {
            if (startSelectable)
            {
                EventSystem.current.SetSelectedGameObject(startSelectable.gameObject);
            }
        }
        
        public virtual void StartCanvas()
        {
            OnCanvasStart();
            HandleAnimator(SHOW_ANIMATION_STRING);
        }

        public virtual void CloseCanvas()
        {
            OnCanvasClose();
            HandleAnimator(HIDE_ANIMATION_STRING);
        }

        #endregion

        #region Private Methods

        private void HandleAnimator(string aTrigger)
        {
            // use DOT Tween to start
            switch (aTrigger)
            {
                case SHOW_ANIMATION_STRING: gameObject.SetActive(true);
                    break;
                case HIDE_ANIMATION_STRING: gameObject.SetActive(false);
                    break;
            }
        }
        
        private void OnCanvasStart()
        {
            onCanvasStart?.Invoke();
        }

        private void OnCanvasClose()
        {
            onCanvasClose?.Invoke();
        }

        #endregion
    }
}