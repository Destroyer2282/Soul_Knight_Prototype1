using Mirror;
using UnityEngine;

public class InstantMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;
    private bool facingRight = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // 🔥 Мгновенная остановка при отсутствии ввода
        if (Mathf.Abs(x) < 0.1f && Mathf.Abs(y) < 0.1f)
        {
            // Ничего не делаем - персонаж стоит
            return;
        }

        // Поворот
        if (x < 0 && facingRight)
        {
            CmdRotatePlayer(false);
            facingRight = false;
        }
        else if (x > 0 && !facingRight)
        {
            CmdRotatePlayer(true);
            facingRight = true;
        }

        // Движение
        Vector3 move = new Vector3(x, y, 0f).normalized * moveSpeed;
        if (x == 0 && y == 0)
        {
            transform.position = transform.position;
        }
        controller.Move(move * Time.deltaTime);
    }

    [Command]
    void CmdRotatePlayer(bool faceRight)
    {
        float yRotation = faceRight ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        RpcSyncRotation(transform.rotation);
    }

    [ClientRpc]
    void RpcSyncRotation(Quaternion newRotation)
    {
        transform.rotation = newRotation;
    }
}