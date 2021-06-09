using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 12;
    private float m_sprintSpeed;
    private float m_baseSpeed;
    [SerializeField]
    private CharacterController m_controller;

    //Jump Variables
    [SerializeField]
    private float m_jumpheight = 2;
    [SerializeField]
    private Transform m_groundChecker;
    [SerializeField]
    private LayerMask m_layerMask;
    [SerializeField]
    private float m_groundDistance = 0.4f;
    [SerializeField]
    private bool m_isGrounded = false;
    [SerializeField]
    private float m_gravity = 20.0f;

    //Axis
    private float m_VelX;
    private float m_VelZ;
    private float m_smoothX;
    private float m_smoothZ;

    private Vector3 Velocity;
    private Vector3 movementRaw;
    private Vector3 movementSmooth;

    // Start is called before the first frame update
    void Start()
    {
        m_controller = GetComponent<CharacterController>();
        m_sprintSpeed = m_speed * 2;
        m_baseSpeed = m_speed;
    }

    // Update is called once per frame
    void Update()
    {
        m_VelX = Input.GetAxisRaw("Horizontal");
        m_VelZ = Input.GetAxisRaw("Vertical");
        m_smoothX = Input.GetAxis("Horizontal");
        m_smoothZ = Input.GetAxis("Vertical");

        movementRaw = new Vector3(m_VelX, 0.0f, m_VelZ);

        m_isGrounded = Physics.CheckSphere(m_groundChecker.position, m_groundDistance, m_layerMask);

        if (Input.GetButton("Sprint"))
        {
            m_speed = m_sprintSpeed;
        } else
        {
            m_speed = m_baseSpeed;
        }

        if (m_isGrounded)
        {
            Velocity = Vector3.zero;
            movementSmooth = new Vector3(m_smoothX, 0.0f, m_smoothZ);
            m_controller.Move(movementRaw * m_speed * Time.deltaTime);
        } else
        {
            m_controller.Move(movementSmooth * m_speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {
            Velocity.y += m_jumpheight;
        } 
        else if(!m_isGrounded)
        {
            Velocity.y += m_gravity * Time.deltaTime;
        }

        m_controller.Move(Velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(m_groundChecker.position, m_groundDistance);
    }
}
