using UnityEngine;

public class PlayerInfector : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [Header("Infectible Detection")]
    [SerializeField] private LayerMask infectableLayer;
    [SerializeField] private Vector2 boxSize = new(3f, 3f);
    [SerializeField] private Vector2 boxOffset = Vector2.zero;
    [Tooltip("Whether to show the box used for infecting in the editor")]
    [SerializeField] private bool displayInfectBox = false;

    private Collider2D m_closestInfectible = null;
    private IInfectible m_currentHost = null;

    void Update()
    {
        m_closestInfectible = GetClosestInfectible();
    }

    public void CheckInfection()
    {
        m_currentHost?.Abandon(this.gameObject);

        if (m_closestInfectible != null)
        {
            var infectible = m_closestInfectible.GetComponentInParent<IInfectible>();
            if (infectible != null)
            {
                infectible.Infect(this.gameObject);
            }
        }
    }

    public void AttemptHostAction()
    {
        m_currentHost?.AttemptHostAction();
    }

    public void SetHost(IInfectible host)
    {
        m_currentHost = host;

        if (host == null)
        {
            playerController.SetSpeedToParasite();
        }
        else
        {
            playerController.SetSpeedToHost(m_currentHost.GetHostSpeed());
        }
    }

    private Collider2D GetClosestInfectible()
    {
        return InteractUtility.GetClosestInteractable((Vector2)transform.position, boxOffset, boxSize, infectableLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (displayInfectBox)
        {
            // Draw the infection box in the Scene view for debugging
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube((Vector2)transform.position + boxOffset, boxSize);
        }
    }
}
