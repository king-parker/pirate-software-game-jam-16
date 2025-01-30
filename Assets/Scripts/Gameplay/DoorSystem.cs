using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private Transform closedPosition;
    [SerializeField] private Transform openClockwisePosition;
    [SerializeField] private Transform openCounterclockwisePosition;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private bool opensClockwise = true;
    [SerializeField] private float rotationDuration = 2f;

    private Quaternion m_previousRotation;
    private Quaternion m_targetRotation;
    private Quaternion m_openRotation;
    private float m_elapsedTime;
    private bool m_isRotationComplete;

    private void Start()
    {
        m_openRotation = opensClockwise ? openClockwisePosition.rotation : openCounterclockwisePosition.rotation;

        SetTargetRotation();
        door.rotation = m_targetRotation;
        SetPreviousRotation();

        m_elapsedTime = rotationDuration;
        m_isRotationComplete = true;
    }

    private void FixedUpdate()
    {
        m_elapsedTime += Time.fixedDeltaTime;

        if (m_elapsedTime < rotationDuration)
        {
            var t = m_elapsedTime / rotationDuration;
            door.rotation = Quaternion.Slerp(m_previousRotation, m_targetRotation, t);
        }
        else if (!m_isRotationComplete)
        {
            door.rotation = m_targetRotation;
            m_isRotationComplete = true;
        }
    }

    public void ToggleDoor()
    {
        // Update open target if it has been updated between clockwise or counter-clockwise
        UpdateOpenTarget();

        // Reset animation state
        m_elapsedTime = m_isRotationComplete ? 0 : rotationDuration - m_elapsedTime;
        m_isRotationComplete = false;

        // Update start and end rotation values
        isOpen = !isOpen;
        SetTargetRotation();
        SetPreviousRotation();
    }

    private void UpdateOpenTarget()
    {
        if (isOpen) { return; }

        m_openRotation = opensClockwise ? openClockwisePosition.rotation : openCounterclockwisePosition.rotation;
    }

    private void SetTargetRotation() { m_targetRotation = isOpen ? m_openRotation : closedPosition.rotation; }

    private void SetPreviousRotation() { m_previousRotation = isOpen ? closedPosition.rotation : m_openRotation; }
}
