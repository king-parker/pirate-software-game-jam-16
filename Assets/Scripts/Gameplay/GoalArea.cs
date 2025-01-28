using UnityEngine;

public class GoalArea : MonoBehaviour
{
    [SerializeField] private Goal goal;
    [SerializeField] private LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollisionUtility.IsInCollisionLayers(collision.gameObject.layer, playerLayer))
        {
            goal.GoalReached();
        }
    }
}
