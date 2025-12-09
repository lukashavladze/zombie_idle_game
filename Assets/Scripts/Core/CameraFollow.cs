using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 10f;

    [Header("Camera Tilt")]
    public float tiltAngle = 15f;   // 👈 how much camera looks down

    private Quaternion fixedRotation;

    void Start()
    {
        // Set fixed downward rotation
        fixedRotation = Quaternion.Euler(tiltAngle, 0f, 0f);
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Smooth position follow
        Vector3 desiredPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Always keep the same rotation
        transform.rotation = fixedRotation;
    }
}
