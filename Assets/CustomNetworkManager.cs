using UnityEngine;
using Mirror;
public class CustomNetworkManager : NetworkManager
{
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        UIManager.Instance.SpawnGroupToogle(); // включаем меню
    }
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        GameObject player = Instantiate(playerPrefab);
        Player pl = player.GetComponent<Player>();

        // Получаем имя из UI
        string name = UIManager.Instance.nameInputField.text;
        if (string.IsNullOrEmpty(name))
        {
            name = "Player" + conn.connectionId; // Имя по умолчанию
        }

        pl.playerName = name;

        NetworkServer.AddPlayerForConnection(conn, player);

        Debug.Log($"Создан игрок с именем: {name}");
    }

}
