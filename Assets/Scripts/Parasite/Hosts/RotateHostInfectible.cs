using UnityEngine;

public class RotateHostInfectible : MonoBehaviour, IInfectible
{
    [SerializeField] private GameObject hostGroup;
    [SerializeField] private Collider2D infectibleArea;

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

    public void AttemptHostAction()
    {
        transform.Rotate(0, 0, 15f);
    }
}
