using UnityEngine;

public class GunUIController : MonoBehaviour
{
    private Quaternion m_Rotation;

    private void Start()
    {
        m_Rotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = m_Rotation;
    }
}
