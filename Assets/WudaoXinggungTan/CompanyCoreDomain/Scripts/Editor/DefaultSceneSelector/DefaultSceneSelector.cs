using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Editor.DefaultSceneSelector
{
    [InitializeOnLoad]
    public static class DefaultSceneSelector
    {
        private const string DEFAULT_SCENE_PATH_KEY = "DefaultScenePathKey";
        private const string HAS_OPENED_PROJECT_BEFORE_KEY = "HasOpenedProjectBeforeKey";
        private const string CORE_SCENE_FILE = "Assets/";
        private const string GAME_SCENE_FILE = "Assets/";
        private const string GAMEPLAY_SCENE_FILE = "Assets/";
        private const string LOBBY_SCENE_FILE = "Assets/";
    
        static DefaultSceneSelector()
        {
            EditorApplication.delayCall += OnLoad;
        }
        
        private static void OnLoad()
        {
            OpenCoreSceneIfHaventBefore();
            SetSavedSceneAsStarting();
        }

        private static void SetSavedSceneAsStarting()
        {
            var path = EditorPrefs.GetString(DEFAULT_SCENE_PATH_KEY, CORE_SCENE_FILE);
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            EditorSceneManager.playModeStartScene = sceneAsset;
        }

        private static void OpenCoreSceneIfHaventBefore()
        {
            var didOpenProjectBefore = EditorPrefs.HasKey(HAS_OPENED_PROJECT_BEFORE_KEY);
            if (didOpenProjectBefore)
            {
                return;
            }

            try // for some reason some people got an error here, so added a try-catch block just in case 
            {
                EditorSceneManager.OpenScene(CORE_SCENE_FILE);
            }
            catch (Exception) { }
            
            EditorPrefs.SetBool(HAS_OPENED_PROJECT_BEFORE_KEY, true);
        }

        [MenuItem("Tools/WudaoXinggungTan/Scene/Select Default Scene", false, 1)]
        private static void SelectDefaultScene()
        {
            var absolutePath = EditorUtility.OpenFilePanel("Select default scene", GetSelectedFolder(), "unity");
            if (string.IsNullOrEmpty(absolutePath))
            {
                return;
            }
            
            var path = GetProjectRelativePath(absolutePath);
            EditorPrefs.SetString(DEFAULT_SCENE_PATH_KEY, path);
            SetSavedSceneAsStarting();
        }
    
        private static string GetSelectedFolder()
        {
            var obj = Selection.activeObject;
            return obj == null ? "Assets" : AssetDatabase.GetAssetPath(obj.GetInstanceID());;
        }
        
        [MenuItem("Tools/WudaoXinggungTan/Scene/Reset Default Scene", false, 2)]
        private static void ResetDefaultScene()
        {
            EditorPrefs.DeleteKey(DEFAULT_SCENE_PATH_KEY);
            EditorSceneManager.playModeStartScene = null;
        }
    
        [MenuItem("Tools/WudaoXinggungTan/Scene/Open/2", false, 3)]
        private static void OpenRootScene()
        {
            EditorApplication.ExitPlaymode();
            EditorSceneManager.OpenScene(CORE_SCENE_FILE);
        }
    
        [MenuItem("Tools/WudaoXinggungTan/Scene/Open/3", false, 4)]
        private static void OpenGameScene()
        {
            EditorApplication.ExitPlaymode();
            EditorSceneManager.OpenScene(GAME_SCENE_FILE);
        }
        
        [MenuItem("Tools/WudaoXinggungTan/Scene/Open/4", false, 5)]
        private static void OpenGamePlayScene()
        {
            EditorApplication.ExitPlaymode();
            EditorSceneManager.OpenScene(GAMEPLAY_SCENE_FILE);
        }
        
        [MenuItem("Tools/WudaoXinggungTan/Scene/Open/5", false, 5)]
        private static void OpenLobbyScene()
        {
            EditorApplication.ExitPlaymode();
            EditorSceneManager.OpenScene(LOBBY_SCENE_FILE);
        }

        private static string GetProjectRelativePath(string absolutePath)
        {
            if (absolutePath.StartsWith(Application.dataPath))
            {
                return "Assets" + absolutePath.Substring(Application.dataPath.Length);
            }

            Debug.LogError("Selected file is not within the project's Assets folder.");
            return null;
        }
    }
}