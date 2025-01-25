using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;       // The gun's transform to follow
    [SerializeField] private float smoothSpeed = 0.125f; // Smoothness of the camera movement
    [SerializeField] private Vector3 offset;        // Offset relative to the gun

    private float m_cameraZ;

    private void Start()
    {
        m_cameraZ = transform.position.z;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // Desired camera position
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = m_cameraZ;

            // Smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }
}
