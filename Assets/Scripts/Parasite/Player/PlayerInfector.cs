using System.Collections;
using System.Collections.Generic;
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
        Collider2D[] hits = Physics2D.OverlapBoxAll((Vector2)transform.position + boxOffset, boxSize, 0f, infectableLayer);

        Collider2D closestHit = null;
        var closestDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            var distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestHit = hit;
                closestDistance = distance;
            }
        }

        return closestHit;
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
