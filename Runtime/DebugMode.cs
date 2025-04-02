#if UNITY_EDITOR || DEVELOPMENT_BUILD
using UnityEngine;
#endif

namespace ToolbarUtils
{
    public static class DebugMode
    {
        public static bool Enabled {
            get
            {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            return PlayerPrefs.GetInt("ToolbarUtils.DebugModeEnabled", 0) == 1;
#else
            return false;
#endif
            }
            set
            {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            PlayerPrefs.SetInt("ToolbarUtils.DebugModeEnabled", value ? 1 : 0);
#else
            return;
#endif
            }
        }
    }
}

