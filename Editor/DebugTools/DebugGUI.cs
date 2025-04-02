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
        static DebugGUI()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            if (DrawDebugToggleEnabled)
            {
                GUILayout.Space(5);
                DrawDebugToggle();
            }
            if (DrawTimescaleSliderEnabled)
            {
                GUILayout.Space(10);
                DrawTimescaleSlider();
            }
        }

        private static void DrawDebugToggle() =>
            DebugMode.Enabled = GUILayout.Toggle(DebugMode.Enabled, new GUIContent("Debug", "Toggles debug mode"), GUILayout.ExpandWidth(false));

        private static void DrawTimescaleSlider()
        {
            GUILayout.Label("TimeScale", GUILayout.ExpandWidth(false));
            Time.timeScale = (float)Math.Round(GUILayout.HorizontalSlider(Time.timeScale, 0, 2, GUILayout.Width(100), GUILayout.ExpandWidth(false)), 1);
            GUILayout.Label(Time.timeScale.ToString(CultureInfo.InvariantCulture));
        }

        private const string DebugToggleMenuName = "Tools/Metsker/ToolbarUtils/Draw Debug Toggle";
        private const string TimescaleSliderMenuName = "Tools/Metsker/ToolbarUtils/Draw Timescale Slider";

        private static bool DrawDebugToggleEnabled {
            get => EditorPrefs.GetInt("ToolbarUtils.DrawDebugToggle", 1) == 1;
            set => EditorPrefs.SetInt("ToolbarUtils.DrawDebugToggle", value ? 1 : 0);
        }

        private static bool DrawTimescaleSliderEnabled {
            get => EditorPrefs.GetInt("ToolbarUtils.DrawTimescaleSlider", 1) == 1;
            set => EditorPrefs.SetInt("ToolbarUtils.DrawTimescaleSlider", value ? 1 : 0);
        }
        
        [MenuItem(DebugToggleMenuName)]
        private static void ShowDebugToggle()
        {
            DrawDebugToggleEnabled = !DrawDebugToggleEnabled;
            
            if (!DrawDebugToggleEnabled)
                DebugMode.Enabled = false;
        }
        
        [MenuItem(DebugToggleMenuName, true)]
        private static bool ShowDebugToggleValidate()
        {
            Menu.SetChecked(DebugToggleMenuName, DrawDebugToggleEnabled);
            return true;
        }
        
        [MenuItem(TimescaleSliderMenuName)]
        private static void ShowTimescaleSlider()
        {
            DrawTimescaleSliderEnabled = !DrawTimescaleSliderEnabled;
        }
        
        [MenuItem(TimescaleSliderMenuName, true)]
        private static bool ShowTimescaleSliderValidate()
        {
            Menu.SetChecked(TimescaleSliderMenuName, DrawTimescaleSliderEnabled);
            return true;
        }
    }
}
