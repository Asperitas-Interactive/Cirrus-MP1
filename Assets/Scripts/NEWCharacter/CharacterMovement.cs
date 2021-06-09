  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 12;
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
    [SerializeField] 
    private Transform m_playerCam;

    //Axis
    [SerializeField]
    private float m_VelX;
    [SerializeField]
    private float m_VelZ;

    private Vector3 Velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        m_controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        m_VelX = Input.GetAxisRaw("Horizontal");
        m_VelZ = Input.GetAxisRaw("Vertical");

        m_isGrounded = Physics.CheckSphere(m_groundChecker.position, m_groundDistance, m_layerMask);

        Vector3 movement = new Vector3(m_VelX, 0.0f, m_VelZ);

        if (movement.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + m_playerCam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            m_controller.Move(moveDir.normalized * (m_speed * Time.deltaTime));
        }

        if (m_isGrounded)
        {
            Velocity = Vector3.zero;
        }

        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {
            Velocity.y += m_jumpheight;
        } else if(!m_isGrounded)
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
