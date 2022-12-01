using System.Collections;
using System;
using Mirror;
using UnityEngine;
using TMPro;

public class PlayerNetworkHandler : Player
{
    [SyncVar]
    public string playerName;

    public TextMeshProUGUI nickText;
    public TextMeshProUGUI chatMessage;

    public static event Action<PlayerNetworkHandler, string> OnMessage;

    private IEnumerator chatRoutine;

    public override void OnStartClient()
    {
        base.OnStartClient();

        nickText.text = playerName;
    }

    [Command]
    public void CmdSend(string message)
    {
        if (message.Trim() != "")
            RpcReceive(message.Trim());
    }

    [ClientRpc]
    public void RpcReceive(string message)
    {
        OnMessage?.Invoke(this, message);
    }

    public void ShowMessageBilboard(string message)
    {
        if (chatRoutine != null)
            StopCoroutine(chatRoutine);

        chatRoutine = ShowMessage();

        chatMessage.text = message;

        StartCoroutine(chatRoutine);
    }

    private IEnumerator ShowMessage()
    {
        Color currentMessageColor = Color.white;

        chatMessage.color = currentMessageColor;

        yield return new WaitForSeconds(3);

        while (chatMessage.color.a >= 0)
        {
            currentMessageColor = new Color(currentMessageColor.r, currentMessageColor.g, currentMessageColor.b, currentMessageColor.a - 0.1f);
            chatMessage.color = currentMessageColor;
            yield return new WaitForSeconds(0.1f);
        }

        chatMessage.text = string.Empty;

        yield break;
    }
}
