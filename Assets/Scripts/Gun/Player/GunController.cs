using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Rigid Body")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float fireForce = 1000f;
    [SerializeField] private float fireTorque = 100f;
    [SerializeField] private float minTorque = 50f;

    [Header("Shot Flash Particles")]
    [SerializeField] private ParticleSystem shotFlashParticles;
    [SerializeField] private Transform flashPoint;

    private KeyCode m_shootKey = KeyCode.Space;
    private bool m_applyRecoil = false;

    private void Update()
    {
        if (Input.GetKeyDown(m_shootKey))
        {
            FireGun();
        }
    }

    private void FixedUpdate()
    {
        if (m_applyRecoil)
        {
            ApplyRecoil();
        }
    }

    private void FireGun()
    {
        var particles = Instantiate(shotFlashParticles, flashPoint.position, flashPoint.rotation);

        m_applyRecoil = true;
    }

    private void ApplyRecoil()
    {
        m_applyRecoil = false;

        var forward = transform.right;
        var recoilForce = -forward * fireForce;

        var direction = Mathf.Sign(forward.x);
        var sine = forward.y;
        var recoilTorque = direction * Mathf.Max(minTorque, Mathf.Abs(sine) * fireTorque);
        /*
         * NOTES: If the gun is perfectly level, then no rotation is applied. Most of the time this
         * should not be an issue except at the start. The following are potential fixes.
         * 1. Ensure the torque is never zero
         * 2. Spawn the gun with a slight rotation at the start
         */ 

        rb.AddForce(recoilForce);
        rb.AddTorque(recoilTorque);
    }
}
