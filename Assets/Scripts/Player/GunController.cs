using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Slider bulletTimeSlider;
    [SerializeField, Range(0f, 1f)] private float bulletTimeScale = 0.5f;
    [SerializeField] private float bulletTimeDuration = 5f;
    [SerializeField] private float bulletTimeRechargeRate = 1f;

    [Header("Bullet")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawn;

    private KeyCode m_shootKey = KeyCode.Space;
    private KeyCode m_bulletTimeKey = KeyCode.LeftShift;
    private bool m_applyRecoil = false;
    private float m_regularFixedDeltaTime;
    private float m_regularTimeScale = 1f;
    private bool m_isBulletTimeActive = false;
    private float m_remainingBulletTime;
    private bool m_isInputEnabled = true;

    private void Start()
    {
        m_regularFixedDeltaTime = Time.fixedDeltaTime;
        m_isBulletTimeActive = false;
        m_remainingBulletTime = bulletTimeDuration;
    }

    private void Update()
    {
        CheckInputs();
        UpdateBulletTimeCharge();
    }

    private void FixedUpdate()
    {
        if (m_applyRecoil)
        {
            ApplyRecoil();
        }

        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxAngularVelocity, maxAngularVelocity);
    }

    public void DisableInputs()
    {
        m_isInputEnabled = false;
    }

    // For UI or Progress Bars
    public float GetBulletTimePercentage()
    {
        return m_remainingBulletTime / bulletTimeDuration;
    }

    private void CheckInputs()
    {
        if (!m_isInputEnabled) { return; }

        if (Input.GetKeyDown(m_shootKey))
        {
            FireGun();
        }

        if (Input.GetKeyDown(m_bulletTimeKey) && m_remainingBulletTime > 0)
        {
            StartBulletTime();
        }
        if (Input.GetKeyUp(m_bulletTimeKey) || m_remainingBulletTime < 0.001)
        {
            StopBulletTime();
        }
    }

    private void UpdateBulletTimeCharge()
    {
        if (!m_isBulletTimeActive)
        {
            if (m_remainingBulletTime < bulletTimeDuration)
            {
                m_remainingBulletTime += bulletTimeRechargeRate * Time.unscaledDeltaTime;
                m_remainingBulletTime = Mathf.Min(m_remainingBulletTime, bulletTimeDuration);
                bulletTimeSlider.value = GetBulletTimePercentage();
            }
            else
            {
                bulletTimeSlider.gameObject.SetActive(false);
            }
        }

        if (m_isBulletTimeActive)
        {
            // Unhide 
            bulletTimeSlider.gameObject.SetActive(true);

            m_remainingBulletTime -= Time.unscaledDeltaTime;
            bulletTimeSlider.value = GetBulletTimePercentage();
        }
    }

    private void FireGun()
    {
        FireBullet();
        CreateFlash();

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
        float bulletTimeFactor = m_isBulletTimeActive ? 1f / bulletTimeScale : 1f;
        recoilForce *= bulletTimeFactor;
        recoilTorque *= bulletTimeFactor;

        rb.AddForce(recoilForce);
        rb.angularVelocity = 0;
        rb.AddTorque(recoilTorque);
    }

    private void StartBulletTime()
    {
        m_isBulletTimeActive = true;
        Time.timeScale = bulletTimeScale;
        Time.fixedDeltaTime = m_regularFixedDeltaTime * bulletTimeScale;
    }

    private void StopBulletTime()
    {
        m_isBulletTimeActive = false;
        Time.timeScale = m_regularTimeScale;
        Time.fixedDeltaTime = m_regularFixedDeltaTime;
    }

    private void FireBullet()
    {
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
    }

    private void CreateFlash()
    {
        Instantiate(shotFlashParticles, flashPoint.position, flashPoint.rotation);
    }
}
