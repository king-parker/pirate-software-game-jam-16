using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerInfector infector;
    [SerializeField] private float speed = 5f;

    private float m_horizontal;
    private float m_vertical;
    private float m_parasiteSpeed;
    private KeyCode m_infectKey = KeyCode.E;
    private KeyCode m_hostActionKey = KeyCode.Space;

    private void Start()
    {
        m_parasiteSpeed = speed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_infectKey))
        {
            infector.CheckInfection();
        }

        if (Input.GetKeyDown (m_hostActionKey))
        {
            infector.AttemptHostAction();
        }

        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");
    }

    public void SetSpeedToHost(float hostSpeed)
    {
        speed = hostSpeed;
    }

    public void SetSpeedToParasite()
    {
        speed = m_parasiteSpeed;
    }

    private void FixedUpdate()
    {
        var movement = new Vector2(m_horizontal, m_vertical);
        if (movement.magnitude > 1) {  movement.Normalize(); }

        rb.velocity = movement * speed;
        //rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}
