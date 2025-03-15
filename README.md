# Toolbar Utils
![image](https://github.com/user-attachments/assets/a6abda06-3b52-4a53-a2f7-abb50f3f1ca3)

Scene navigation and debug utils for unity toolbar

## Dependencies
This package requires https://github.com/marijnz/unity-toolbar-extender for it to work. Since Unity does not support git dependencies for packages, you need to install it separately.

It also currently requires Unity new Input system, because I don't see a point to support the old one.

## Features:
- Easy navigation between the scenes in build, hold alt to load in additive mode [Buttons]
- Redirect to the first scene in build on play mode enter [Toggle]
- Debug (Editor only) [Toggle]
- Time scale [Slider]

## Debug toggle API usage example:
```
#if UNITY_EDITOR
            if (ToolbarUtils.DebugGUI.DebugMode)
            {
                // Bootstrap in debug mode
                return;
            }
#endif
```
