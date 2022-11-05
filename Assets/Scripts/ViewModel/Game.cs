using UnityEngine;

public class Game
{
    public static IInputSevice InputService;

    public Game()
    {
        RegisterInputservice();
    }

    private static void RegisterInputservice()
    {
#if PLATFORM_STANDALONE
        InputService = new DesktopInputService();
#endif
    }
}
