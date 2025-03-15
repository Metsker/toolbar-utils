using System;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

namespace ToolbarUtils.DebugTools
{
    [InitializeOnLoad]
    public static class DebugGUI
    {
        public static bool DebugMode {
            get => EditorPrefs.GetInt("DebugMode", 0) == 1;
            private set => EditorPrefs.SetInt("DebugMode", value ? 1 : 0);
        }
        
        static DebugGUI()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            GUILayout.Space(5);
            
            DrawDebugToggle();
            
            GUILayout.Space(10);

            DrawTimescaleSlider();
        }

        private static void DrawDebugToggle() =>
            DebugMode = GUILayout.Toggle(DebugMode, new GUIContent("Debug", "Enables debug mode"), GUILayout.ExpandWidth(false));

        private static void DrawTimescaleSlider()
        {
            GUILayout.Label("TimeScale", GUILayout.ExpandWidth(false));
            Time.timeScale = (float)Math.Round(GUILayout.HorizontalSlider(Time.timeScale, 0, 2, GUILayout.Width(100), GUILayout.ExpandWidth(false)), 1);
            GUILayout.Label(Time.timeScale.ToString(CultureInfo.InvariantCulture));
        }
    }
}
