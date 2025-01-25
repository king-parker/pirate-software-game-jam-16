using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private Transform openPosition;
    [SerializeField] private Transform closePosition;
    [SerializeField] private float speed = 5;
    [SerializeField] private bool isDoorOpen;

    private Vector3 m_targetPos;
    [SerializeField] private bool m_isDoorMoving;

    private void Start()
    {
        if (isDoorOpen) { transform.position = openPosition.position; }
        else { transform.position = closePosition.position; }
    }

    private void FixedUpdate()
    {
        if (m_isDoorMoving)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, m_targetPos, speed * Time.fixedDeltaTime);
            transform.position = newPos;

            if (Vector3.Distance(transform.position, m_targetPos) < 0.01f)
            {
                m_isDoorMoving = false;
                transform.position = m_targetPos;
            }
        }
    }

    public void OpenCloseDoor()
    {
        if (m_isDoorMoving) { return; }
        if (isDoorOpen) { CloseDoor(); }
        else { OpenDoor(); }
    }

    private void OpenDoor()
    {
        m_isDoorMoving = true;
        m_targetPos = openPosition.position;
        isDoorOpen = true;
    }

    private void CloseDoor()
    {
        m_isDoorMoving = true;
        m_targetPos = closePosition.position;
        isDoorOpen = false;
    }
}
