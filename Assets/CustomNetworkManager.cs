using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        UIManager.Instance.SpawnGroupToogle();
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // Получаем позицию и rotation из spawn point
        Transform startPos = GetStartPosition();

        // Создаём игрока на позиции spawn point
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab); // fallback если нет spawn points

        Player pl = player.GetComponent<Player>();

        // Устанавливаем имя
        if (conn.authenticationData != null)
        {
            pl.playerName = (string)conn.authenticationData;
        }
        else
        {
            pl.playerName = "Player" + conn.connectionId;
        }

        NetworkServer.AddPlayerForConnection(conn, player);

        Debug.Log($"Игрок {pl.playerName} заспавнился на позиции: {player.transform.position}");
    }
}