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
        // �������� ������� � rotation �� spawn point
        Transform startPos = GetStartPosition();

        // ������ ������ �� ������� spawn point
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab); // fallback ���� ��� spawn points

        Player pl = player.GetComponent<Player>();

        // ������������� ���
        if (conn.authenticationData != null)
        {
            pl.playerName = (string)conn.authenticationData;
        }
        else
        {
            pl.playerName = "Player" + conn.connectionId;
        }

        NetworkServer.AddPlayerForConnection(conn, player);

        Debug.Log($"����� {pl.playerName} ����������� �� �������: {player.transform.position}");
    }
}