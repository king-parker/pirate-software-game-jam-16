using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private LayerMask bulletLayer;
    [SerializeField] private DoorSystem[] doors;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollisionUtility.IsInCollisionLayers(collision.gameObject.layer, bulletLayer))
        {
            var audioManager = GameObject.FindGameObjectWithTag(AudioManager.TAG).GetComponent<AudioManager>();
            audioManager.PlayTargetBreak(transform.position.x);

            // Destroy bullet
            Destroy(collision.gameObject);

            // Open doors
            foreach (var door in doors)
            {
                if (door != null)
                {
                    door.ToggleDoor();
                }
            }

            // Break target
            Destroy(this.gameObject);
        }
    }
}
