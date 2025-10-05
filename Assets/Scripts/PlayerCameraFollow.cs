using UnityEngine;
using Mirror;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null)
        {
            FindLocalPlayer();
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    void FindLocalPlayer()
    {
        foreach (var player in FindObjectsOfType<NetworkIdentity>())
        {
            if (player.isLocalPlayer)
            {
                target = player.transform;
                break;
            }
        }
    }
}