using GrabCoin.Services.Chat;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    Game _game;

    [SerializeField] private ChatWindow Chat;

    void Start()
    {
        _game = new Game();

        Game.ChatReference = Chat;

        DontDestroyOnLoad(gameObject);
    }
}
