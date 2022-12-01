using GrabCoin.Services.Chat;
using UnityEngine;

public class Game
{
    public static IInputSevice InputService;

    public static ChatWindow ChatReference;

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
        GameSettings.activeKeyCode.Add(GameSettings.OpenChat, true);
    }
}
