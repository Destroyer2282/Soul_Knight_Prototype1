using UnityEngine;
using Mirror;
public class CustomNetworkManager : NetworkManager
{
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        UIManager.Instance.SpawnGroupToogle(); // �������� ����
    }
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        GameObject player = Instantiate(playerPrefab);
        Player pl = player.GetComponent<Player>();

        // �������� ��� �� UI
        string name = UIManager.Instance.nameInputField.text;
        if (string.IsNullOrEmpty(name))
        {
            name = "Player" + conn.connectionId; // ��� �� ���������
        }

        pl.playerName = name;

        NetworkServer.AddPlayerForConnection(conn, player);

        Debug.Log($"������ ����� � ������: {name}");
    }

}
