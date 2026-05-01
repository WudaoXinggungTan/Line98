using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuScripts.MVC.UI.MainMenu
{
    public class MainMenuUICanvasView : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Canvas screenCanvas;

        private Action onStartButtonClicked;
        private Action onQuitButtonClicked;

        public void InitEntryPoint(Camera uiCamera, Action onStartButtonClicked, Action onQuitButtonClicked)
        {
            screenCanvas.worldCamera = uiCamera;
            startButton.onClick.AddListener(OnStartButtonClicked);
            exitButton.onClick.AddListener(OnQuitButtonClicked);

            this.onStartButtonClicked = onStartButtonClicked;
            this.onQuitButtonClicked = onQuitButtonClicked;
        }

        private void OnStartButtonClicked()
        {
            onStartButtonClicked?.Invoke();
        }

        private void OnQuitButtonClicked()
        {
            onQuitButtonClicked?.Invoke();
        }
    }
}