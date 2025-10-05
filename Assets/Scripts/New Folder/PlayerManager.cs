using Mirror;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    private static PlayerManager _instance;

    public static PlayerManager Instance
    {
        get
        {
            return _instance;

        }

    }

    #endregion
    private void Awake()
    {
        _instance = this;
    }
    [SerializeField] private NetworkManager netManager;
    public void SpawnPlayer()
    {
        if (!NetworkClient.isConnected) return;

        if (!NetworkClient.ready)
            NetworkClient.Ready();

        if (NetworkClient.localPlayer == null)
        {
            NetworkClient.AddPlayer();
            UIManager.Instance.SpawnGroupToogle();
        }
    }
}
