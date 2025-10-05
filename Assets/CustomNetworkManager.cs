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
        // ������ ������
        GameObject player = Instantiate(playerPrefab);

        // �������� ������ �� Player
        Player pl = player.GetComponent<Player>();

        // ������ ��� �� UIManager (�� ������� ��� ���� �������)
        pl.playerName = UIManager.Instance.nameInputField.text;

        // ��������� ������ � ����
        NetworkServer.AddPlayerForConnection(conn, player);
    }

}
