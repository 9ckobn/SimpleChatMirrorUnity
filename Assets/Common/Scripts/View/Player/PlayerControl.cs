using GrabCoin.Services.Chat;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nickField;

    private ChatWindow Chat;

    Player _player;

    void Start()
    {
        _player = GetComponent<Player>();

        Chat = Game.ChatReference;
    }

    void Update()
    {
        if (Input.GetKeyDown(GameSettings.OpenChat))
        {

            if (GameSettings.activeKeyCode[GameSettings.OpenChat])
                Chat.OpenChat();

            SwitchInputMode(false, GameSettings.OpenChat);
        }

        if (Input.GetKeyDown(GameSettings.Escape))
        {
            Chat.CloseChat();

            SwitchInputMode(true, GameSettings.OpenChat);
        }
    }

    public void SwitchInputMode(bool enable, KeyCode keyToDisable)
    {
        GameSettings.activeKeyCode[keyToDisable] = enable;
        _player.enabledInput = enable;
        Cursor.visible = !enable;
    }
}
