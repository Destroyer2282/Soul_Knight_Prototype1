using UnityEngine;
using Mirror;

public class InputManager : MonoBehaviour
{
    #region Singleton
    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    private Vector3 movementVector = new Vector3();
    [SerializeField] private Player playerObj;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if (playerObj)
            MoveInput();
    }

    public void SetPlayer(Player pl)
    {
        playerObj = pl;
    }

    private void MoveInput()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");
        playerObj.CmdMovePlayer(movementVector);
        //playerObj.MovePlayer(movementVector);
    }

    public void SpawnPlayer()
    {
        if (!UIManager.Instance.CheckName()) return;

        // Сохраняем имя в authenticationData перед подключением
        string playerName = UIManager.Instance.nameInputField.text;
        NetworkClient.connection.authenticationData = playerName;

        PlayerManager.Instance.SpawnPlayer();
        UIManager.Instance.SpawnGroupToogle();

        Debug.Log($"Клиент отправил имя: {playerName}");
    }
}