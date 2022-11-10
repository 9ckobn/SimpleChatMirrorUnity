using UnityEngine;

public class Game
{
    public static IInputSevice InputService;

    public Game()
    {
#if PLATFORM_STANDALONE
        RegisterInputservice();
        RegisterInputKeys();
#endif
    }

    private static void RegisterInputservice()
    {
        InputService = new DesktopInputService();
    }

    private static void RegisterInputKeys()
    {
        GameSettings.activeKeyCode.Add(GameSettings.LeftMouseButton, true);
    }
}
