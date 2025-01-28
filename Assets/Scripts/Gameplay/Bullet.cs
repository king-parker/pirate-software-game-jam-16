using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 50;
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private Rigidbody2D rb;

    private void FixedUpdate()
    {
        var velocity = transform.right * speed;
        rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsInCollisionLayers(collision.gameObject.layer))
        {
            Destroy(this.gameObject);
        }
    }

    private bool IsInCollisionLayers(int layer)
    {
        return ((1 << layer) & collisionLayers) != 0;
    }
}
