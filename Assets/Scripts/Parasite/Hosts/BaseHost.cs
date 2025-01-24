using UnityEngine;

public abstract class BaseHost : MonoBehaviour, IInfectible
{
    [Header("Base Host Properties")]
    [SerializeField] protected GameObject hostGroup;
    [SerializeField] protected Collider2D infectibleArea;
    [SerializeField] protected float hostSpeed = 5f;

    public void Infect(GameObject player)
    {
        // Place player at top of host
        player.transform.position = this.transform.position + new Vector3(0, .4375f);
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        // Reparent host to player so they move with the player
        transform.SetParent(player.transform);

        // Turn off infectible collider so it is no longer detected
        infectibleArea.enabled = false;

        // Notify player of its host
        player.GetComponent<PlayerInfector>().SetHost(this);
    }

    public void Abandon(GameObject player)
    {
        transform.SetParent(hostGroup.transform);
        infectibleArea.enabled = true;
        player.GetComponent<PlayerInfector>().SetHost(null);
    }

    public float GetHostSpeed() { return hostSpeed; }

    public abstract void AttemptHostAction();
}