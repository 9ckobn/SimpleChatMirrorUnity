using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public static Dictionary<KeyCode, bool> activeKeyCode = new Dictionary<KeyCode, bool>();
 
    public const KeyCode LeftMouseButton = KeyCode.Mouse0;
    public const KeyCode OpenChat = KeyCode.T;
    public const KeyCode Escape = KeyCode.Escape;

    public static void DisableKeyboardInput()
    {

    }
}
