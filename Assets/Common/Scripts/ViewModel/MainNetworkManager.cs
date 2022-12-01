using UnityEngine;
using Mirror;
using GrabCoin.Services.Chat;

public class MainNetworkManager : NetworkManager
{
    [SerializeField]
    private ChatWindow chatWindow = null;

    public string PlayerName { get; set; }

    public void SetHostname(string hostname)
    {
        networkAddress = hostname;
    }

    public struct CreatePlayerMessage : NetworkMessage
    {
        public string name;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreatePlayer);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        CreatePlayerMessage playerMessage = new CreatePlayerMessage { name = PlayerName };

        NetworkClient.Send(playerMessage);

        // Tell the server to create a player with this name.
        //NetworkConnection.Send(new CreatePlayerMessage { name = PlayerName });
    }

    private void OnCreatePlayer(NetworkConnectionToClient connection, CreatePlayerMessage createPlayerMessage)
    {
        // Create a gameobject using the name supplied by client.
        GameObject playergo = Instantiate(playerPrefab);

        var player = playergo.GetComponent<PlayerNetworkHandler>();

        player.playerName = createPlayerMessage.name;

        // Set it as the player.
        NetworkServer.AddPlayerForConnection(connection, playergo);

        chatWindow.gameObject.SetActive(true);
    }
}
