using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Rigid Body")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float fireForce = 1000f;
    [SerializeField] private float fireTorque = 100f;
    [SerializeField] private float minTorque = 50f;
    [SerializeField] private float maxAngularVelocity = 500f;

    [Header("Shot Flash Particles")]
    [SerializeField] private ParticleSystem shotFlashParticles;
    [SerializeField] private Transform flashPoint;

    [Header("Bulle Time")]
    [SerializeField][Range(0f, 1f)] private float bulletTimeScale = 0.5f;

    private KeyCode m_shootKey = KeyCode.Space;
    private KeyCode m_bulletTimeKey = KeyCode.LeftShift;
    private bool m_applyRecoil = false;
    private float m_regularFixedDeltaTime;
    private float m_regularTimeScale = 1f;
    private bool m_bulletTimeActive;

    private void Start()
    {
        m_regularFixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_shootKey))
        {
            FireGun();
        }

        if (Input.GetKeyDown(m_bulletTimeKey))
        {
            StartBulletTime();
        }
        if (Input.GetKeyUp(m_bulletTimeKey))
        {
            StopBulletTime();
        }
    }

    private void FixedUpdate()
    {
        if (m_applyRecoil)
        {
            ApplyRecoil();
        }

        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxAngularVelocity, maxAngularVelocity);
    }

    private void FireGun()
    {
        var particles = Instantiate(shotFlashParticles, flashPoint.position, flashPoint.rotation);

        m_applyRecoil = true;
    }

    private void ApplyRecoil()
    {
        m_applyRecoil = false;

        var forward = -transform.right;
        var recoilForce = -forward * fireForce;

        var direction = Mathf.Sign(forward.x);
        var sine = forward.y;
        var recoilTorque = direction * Mathf.Max(minTorque, Mathf.Abs(sine) * fireTorque);

        // Scale forces up durring bullet time to maintain consistent physics
        float bulletTimeFactor = m_bulletTimeActive ? 1f / bulletTimeScale : 1f;
        recoilForce *= bulletTimeFactor;
        recoilTorque *= bulletTimeFactor;

        rb.AddForce(recoilForce);
        rb.angularVelocity = 0;
        rb.AddTorque(recoilTorque);
    }

    private void StartBulletTime()
    {
        m_bulletTimeActive = true;
        Time.timeScale = bulletTimeScale;
        Time.fixedDeltaTime = m_regularFixedDeltaTime * bulletTimeScale;
    }

    private void StopBulletTime()
    {
        m_bulletTimeActive = false;
        Time.timeScale = m_regularTimeScale;
        Time.fixedDeltaTime = m_regularFixedDeltaTime;
    }
}
