using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 10f;

    private Quaternion fixedRotation;

    void Start()
    {
        // Save the initial rotation of the camera (so it never rotates again)
        fixedRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Follow position only
        Vector3 desiredPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Keep rotation fixed (do not rotate toward player)
        transform.rotation = fixedRotation;
    }
}
