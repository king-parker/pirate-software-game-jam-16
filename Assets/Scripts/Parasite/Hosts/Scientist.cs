using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : BaseHost
{
    [SerializeField] private BoxCollider2D interactArea;
    [SerializeField] private LayerMask interactableLayer;

    private Collider2D m_closestInteractable;

    private void Update()
    {
        m_closestInteractable = InteractUtility.GetClosestInteractable(transform.position, Vector2.zero, interactArea.size, interactableLayer);
    }

    public override void AttemptHostAction()
    {
        AttemptInteraction();
    }

    private void AttemptInteraction()
    {
        if (m_closestInteractable != null)
        {
            var interactable = m_closestInteractable.GetComponentInParent<IScientistInteractable>();
            interactable?.Interact();
        }
    }
}
