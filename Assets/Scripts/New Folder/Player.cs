using UnityEngine;
using Mirror;
using TMPro;

public class Player : NetworkBehaviour
{
    [SyncVar][SerializeField] private float speed;
    [SyncVar(hook = nameof(OnRotationChanged))]
    private Quaternion syncedRotation;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    private Rigidbody2D rb;

    [Header("Name Display")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Canvas nameCanvas;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (isClient && isLocalPlayer)
        {
            SetInputManagerPlayer();
        }

        if (isServer)
        {
            speed = 3;
        }
    }

    private void LateUpdate()
    {
        // Billboard эффект - имя всегда смотрит на камеру
        if (nameCanvas != null && Camera.main != null)
        {
            nameCanvas.transform.rotation = Camera.main.transform.rotation;
        }
    }

    // Вызывается на всех клиентах когда имя синхронизируется
    private void OnNameChanged(string oldName, string newName)
    {
        UpdateNameDisplay(newName);
    }

    private void UpdateNameDisplay(string name)
    {
        if (nameText != null)
        {
            nameText.text = name;
            Debug.Log($"Имя игрока обновлено: {name}");
        }
    }

    private void OnRotationChanged(Quaternion oldRotation, Quaternion newRotation)
    {
        transform.rotation = newRotation;
    }

    private void SetInputManagerPlayer()
    {
        InputManager.Instance.SetPlayer(this);
        UIManager.Instance.SpawnGroupToogle();
    }

    [Command]
    public void CmdMovePlayer(Vector3 movementVector)
    {
        if (movementVector.x < 0)
        {
            syncedRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            syncedRotation = Quaternion.Euler(0, 0, 0);
        }
        transform.rotation = syncedRotation;
        rb.AddForce(new Vector2(movementVector.x, movementVector.y).normalized * speed);
    }

    public void MovePlayer(Vector3 movementVector)
    {
        rb.AddForce(movementVector.normalized * speed);
    }
}