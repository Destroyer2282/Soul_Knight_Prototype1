using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar] [SerializeField] private float speed;

    private Rigidbody2D rb;

    public string playerName;
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        if(isClient && isLocalPlayer)
        {

            SetInputManagerPlayer();
        }
        if(isServer)
        {

            speed = 3;

        }

    }
    private void SetInputManagerPlayer()
    {
        InputManager.Instance.SetPlayer(this);
        UIManager.Instance.SpawnGroupToogle();
    }

    [Command]
    public void CmdMovePlayer(Vector3 movementVector)
    {
        if(movementVector.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        rb.AddForce(new Vector2(movementVector.x, movementVector.y).normalized * speed);
        
    }

    public void MovePlayer(Vector3 movementVector)
    {
        rb.AddForce(movementVector.normalized * speed);
    }
}
