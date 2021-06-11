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
    [SerializeField] 
    private Transform m_playerCam;

    //Axis
    private float m_VelX;
    private float m_VelZ;
    private float m_smoothX;
    private float m_smoothZ;

    private Vector3 Velocity;
    private Vector3 movementRaw;
    private Vector3 movementSmooth;
    private float movementAir;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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

        if (m_isGrounded) //Whenever player is grounded
        {
            Velocity = Vector3.zero;
            movementSmooth = new Vector3(m_smoothX, 0.0f, m_smoothZ);
            movementAir = m_speed;
        }


        if (m_isGrounded && movementRaw.magnitude > 0.1f) //If grounded and moving
        {
            float targetAngle = Mathf.Atan2(movementRaw.x, movementRaw.z) * Mathf.Rad2Deg + m_playerCam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            m_controller.Move(moveDir.normalized * (m_speed * Time.deltaTime));

        } else if ((!m_isGrounded && movementSmooth.magnitude > 0.1f && Velocity.y > 0.0f)) { //If in the air, moving and ascending

            movementSmooth = new Vector3(m_smoothX, 0.0f, m_smoothZ);
            float targetAngle = Mathf.Atan2(movementRaw.x, movementRaw.z) * Mathf.Rad2Deg + m_playerCam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            m_controller.Move(moveDir.normalized * (movementAir * Time.deltaTime));

        } else if (!m_isGrounded && movementSmooth.magnitude > 0.1f && Velocity.y < 0.0f) //If in the air and moving and descending
        {
            float targetAngle = Mathf.Atan2(movementSmooth.x, movementSmooth.z) * Mathf.Rad2Deg + m_playerCam.eulerAngles.y;

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            m_controller.Move(moveDir * (movementAir * Time.deltaTime));

        } else if (!m_isGrounded)//If in the air and not moving
        {
            m_controller.Move(movementSmooth * (movementAir * Time.deltaTime));
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
