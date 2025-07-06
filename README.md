# Toolbar Utils
![image](https://github.com/user-attachments/assets/a6abda06-3b52-4a53-a2f7-abb50f3f1ca3)

Scene navigation and debug utils for unity toolbar

## Dependencies
This package requires https://github.com/marijnz/unity-toolbar-extender for it to work. Since Unity does not support git dependencies for packages, you need to install it separately.

## Features:
- Easy navigation between the scenes in build, hold alt to load in additive mode [Buttons]
- Redirect to the first scene in build on play mode enter [Toggle]
- Debug (Works only in Development build and Editor) [Toggle]
- Time scale [Slider]
- Toggle any of the above modules visibility via Tools/Metsker/ToolbarUtils

## Debug mode API usage example:
```
using ToolbarUtils;

public class Example
{
    void Foo()
    {
        if(DebugMode.Enabled)
        {
            //Your debug logic
        }
        else
        {
            //Your release logic
        }
    }
}
```
