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
            get => EditorPrefs.GetBool("RedirectScene");
            set => EditorPrefs.SetBool("RedirectScene", value);
        }
        
        static SceneNavGUI()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();

            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                string path = scene.path;
                string name = Path.GetFileNameWithoutExtension(path);

                if (!GUILayout.Button(new GUIContent(name, "Hold Alt to open in additive mode")))
                    continue;
                
                if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    return;
                        
                EditorSceneManager.OpenScene(path, !Keyboard.current.altKey.isPressed ? OpenSceneMode.Single : OpenSceneMode.Additive);
            }
            
            Redirect = GUILayout.Toggle(Redirect, new GUIContent("Redirect", "Redirect to the first scene in build on play"));
            SetPlayModeStartScene(Redirect ? EditorBuildSettings.scenes[0].path : null);
            
            GUILayout.EndHorizontal();
        }

        private static void SetPlayModeStartScene([CanBeNull]string scenePath)
        {
            SceneAsset startScene = string.IsNullOrEmpty(scenePath) ? null : AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
            EditorSceneManager.playModeStartScene = startScene;
        }
    }
}