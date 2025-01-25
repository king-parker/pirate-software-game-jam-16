using UnityEngine;

public class InteractUtility
{
    public static Collider2D GetClosestInteractable(Vector2 actorPosition, Vector2 boxOffset, Vector2 boxSize, LayerMask interactableLayer)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(actorPosition + boxOffset, boxSize, 0f, interactableLayer);

        Collider2D closestHit = null;
        var closestDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            var distance = Vector2.Distance(actorPosition, hit.transform.position);
            if (distance < closestDistance)
            {
                closestHit = hit;
                closestDistance = distance;
            }
        }

        return closestHit;
    }
}