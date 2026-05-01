using UnityEditor;
using UnityEngine;
using CompanyCoreScripts.Utils;

namespace CompanyCoreScripts.Editor.Portfolio
{
    [InitializeOnLoad]
    public class PortfolioWindow : EditorWindow
    {
        private const string DONT_SHOW_AGAIN_KEY = "DontShowAgain";
        private const string PDF_PATH = "Assets/WudaoXinggungTan/Portfolio.pdf";
        private static readonly Vector2 WINDOW_SIZE = new Vector2(400, 220);
        
        private static bool _isDontShowAgain;

        static PortfolioWindow()
        {
            EditorApplication.delayCall += TryOpenWindowOnStartup;
        }
        
        private static void TryOpenWindowOnStartup()
        {
            if (EditorPrefs.HasKey(DONT_SHOW_AGAIN_KEY) && EditorPrefs.GetBool(DONT_SHOW_AGAIN_KEY))
            {
                return;
            }

            var windows = Resources.FindObjectsOfTypeAll<PortfolioWindow>();
            var isWindowAlreadyOpen = windows is {Length: > 0};
            if (!isWindowAlreadyOpen)
            {
                ShowWindow();
            }
        }

        [MenuItem("Tools/WudaoXinggungTan/Portfolio Window")]
        public static void ShowWindow()
        {
            var window = GetWindow<PortfolioWindow>();
            window.titleContent = new GUIContent("Wudao Xinggung Tan Portfolio");
            _isDontShowAgain = EditorPrefs.GetBool(DONT_SHOW_AGAIN_KEY);
            window.Show();
        }

        private void OnGUI()
        {
            DrawHeaderGUI();
            GUILayout.Space(10);
            DrawOpenPortfolioWindowGUI();
            GUILayout.Space(10);
            DrawAutoOpenGUI();
            ResizeToFitContents();
        }

        private static void DrawHeaderGUI()
        {
            var headerStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(0.2f, 0.6f, 0.8f) }
            };
    
            GUILayout.Label("🎉 My name is Wudao Xinggung Tan! 🎉", headerStyle);
            GUILayout.Space(10);
            GUILayout.Label("Welcome to my portfolio!\nI hope you'll find this project interesting. Enjoy!", EditorStyles.wordWrappedLabel);
        }

        private static void DrawOpenPortfolioWindowGUI()
        {
            GUILayout.BeginVertical("box");
            GUILayout.Label("📖 Portfolio PDF", EditorStyles.boldLabel);
    
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Checkout My Portfolio -->", GUILayout.Height(40)))
            {
                IOUtils.OpenFile(PDF_PATH);
            }
            GUI.backgroundColor = Color.white;

            GUILayout.EndVertical();
        }

        private static void DrawAutoOpenGUI()
        {
            GUILayout.BeginVertical("box");
            GUILayout.Label("⚙️ Settings", EditorStyles.boldLabel);
    
            var previousIsDontShowAgain = _isDontShowAgain;
            _isDontShowAgain = GUILayout.Toggle(_isDontShowAgain, "Dont Show Again This Window?");
            GUILayout.Label("Can always be found under the tab Tools/WudaoXinggungTan/Portfolio Window", EditorStyles.helpBox);
            
            bool didDontShowAgainChanged = previousIsDontShowAgain != _isDontShowAgain;

            if (didDontShowAgainChanged)
            {
                EditorPrefs.SetBool(DONT_SHOW_AGAIN_KEY, _isDontShowAgain);
            }

            GUILayout.EndVertical();
        }
        
        private void ResizeToFitContents()
        {
            minSize = WINDOW_SIZE;
            maxSize = WINDOW_SIZE;
        }
    }
}
