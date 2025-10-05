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
        // создаём игрока
        GameObject player = Instantiate(playerPrefab);

        // получаем ссылку на Player
        Player pl = player.GetComponent<Player>();

        // достаём имя из UIManager (на клиенте оно было введено)
        pl.playerName = UIManager.Instance.nameInputField.text;

        // добавляем игрока в сеть
        NetworkServer.AddPlayerForConnection(conn, player);
    }

}
