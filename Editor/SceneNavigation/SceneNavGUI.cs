using System.IO;
using JetBrains.Annotations;
using UnityToolbarExtender;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ToolbarUtils.SceneNavigation
{
    [InitializeOnLoad]
    public static class SceneNavGUI
    {
        private static bool Redirect {
            get => EditorPrefs.GetBool("ToolbarUtils.RedirectScene");
            set => EditorPrefs.SetBool("ToolbarUtils.RedirectScene", value);
        }
        
        static SceneNavGUI()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            if (!DrawSceneNavEnabled)
                return;
            
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();

            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (!scene.enabled)
                    continue;
                
                string path = scene.path;
                string name = Path.GetFileNameWithoutExtension(path);

                if (!GUILayout.Button(new GUIContent(name, "Hold Alt to open in additive mode")))
                    continue;
                
                if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    return;
                        
                EditorSceneManager.OpenScene(path, !Keyboard.current.altKey.isPressed ? OpenSceneMode.Single : OpenSceneMode.Additive);
            }
            
            string firstScenePath = EditorBuildSettings.scenes.Length > 0 ? EditorBuildSettings.scenes[0].path : null;

            if (string.IsNullOrEmpty(firstScenePath))
            {
                GUILayout.EndHorizontal();
                return;
            }
            Redirect = GUILayout.Toggle(Redirect, new GUIContent("Redirect", "Enters play mode from the first scene in build settings"));
            
            SetPlayModeStartScene(Redirect ? firstScenePath : null);
            
            GUILayout.EndHorizontal();
        }

        private static void SetPlayModeStartScene([CanBeNull] string scenePath)
        {
            SceneAsset startScene = string.IsNullOrEmpty(scenePath) ? null : AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
            EditorSceneManager.playModeStartScene = startScene;
        }
        
        private const string SceneNavMenuName = "Tools/Metsker/ToolbarUtils/Scene Navigation";

        private static bool DrawSceneNavEnabled {
            get => EditorPrefs.GetInt("ToolbarUtils.DrawSceneNav", 1) == 1;
            set => EditorPrefs.SetInt("ToolbarUtils.DrawSceneNav", value ? 1 : 0);
        }
        
        [MenuItem(SceneNavMenuName)]
        private static void ShowSceneNav()
        {
            DrawSceneNavEnabled = !DrawSceneNavEnabled;
            
            if (!DrawSceneNavEnabled)
            {
                Redirect = false;
                SetPlayModeStartScene(null);
            }
        }
        
        [MenuItem(SceneNavMenuName, true)]
        private static bool ShowSceneNavValidate()
        {
            Menu.SetChecked(SceneNavMenuName, DrawSceneNavEnabled);
            return true;
        }
    }
}
