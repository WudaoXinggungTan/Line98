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

        private Canvas canvas;

        #endregion

        #region Actions

        private Action onCanvasStart;
        private Action onCanvasClose;

        #endregion

        #region Virtual Methods

        public virtual void InitEntryPoint()
        {
            canvas = GetComponent<Canvas>();
        }

        public virtual void StartEntryPoint()
        {
        }

        public virtual void InitExitPoint()
        {
        }

        public virtual void StartCanvas()
        {
            OnCanvasStart();
            canvas.enabled = true;
            if (startSelectable)
            {
                EventSystem.current.SetSelectedGameObject(startSelectable.gameObject);
            }
            
        }

        public virtual void CloseCanvas()
        {
            OnCanvasClose();
            canvas.enabled = false;
        }

        #endregion

        #region Private Methods

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