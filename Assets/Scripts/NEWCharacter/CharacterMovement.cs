  using System.Collections;
using System.Collections.Generic;
  using Cinemachine;
  using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook m_freeLook;
    [SerializeField]
    private float m_speed = 12;
    private float m_sprintSpeed;
    private float m_baseSpeed;

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

    private bool isGliding = false;

    //Axis
    private float m_VelX;
    private float m_VelZ;
    private float m_smoothX;
    private float m_smoothZ;

    private Vector3 Velocity;
    private Vector3 movementRaw;
    private Vector3 movementSmooth;
    private float movementAir;

    RaycastHit hitInfo;

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
        GetAxis();

        m_isGrounded = Physics.Raycast(transform.position, -Vector3.up, out hitInfo, m_groundDistance, m_layerMask);

        SpeedControl();

        GlideControl();

        MovementControl();

        ApplyGravity();
    }

    void GetAxis()
    {
        m_VelX = Input.GetAxisRaw("Horizontal");
        m_VelZ = Input.GetAxisRaw("Vertical");
        m_smoothX = Input.GetAxis("Horizontal");
        m_smoothZ = Input.GetAxis("Vertical");

        movementRaw = new Vector3(m_VelX, 0.0f, m_VelZ);
    }

    void SpeedControl()
    {
        if (Input.GetButton("Sprint"))
        {
            m_speed = m_sprintSpeed;
        }
        else
        {
            m_speed = m_baseSpeed;
        }

        if (m_isGrounded) //Whenever player is grounded
        {
            Velocity = Vector3.zero;
            movementSmooth = new Vector3(m_smoothX, 0.0f, m_smoothZ);
            movementAir = m_speed;
            m_freeLook.m_XAxis.m_MaxSpeed = 450;
        }
    }

    void GlideControl()
    {
        if (!m_isGrounded && Input.GetButtonDown("Jump"))
        {
            isGliding = true;
            //No movement Airspeed while gliding
            movementAir = 0;
        }
        if (!m_isGrounded && Input.GetButtonUp("Jump"))
        {
            isGliding = false;
            //Return to base speed on release
            movementAir = m_baseSpeed;
        }

        if (m_isGrounded)
        {
            isGliding = false;
        }
    }

   void MovementControl()
    {
        if (m_isGrounded && movementRaw.magnitude > 0.1f) //If grounded and moving
        {
            float targetAngle = Mathf.Atan2(movementRaw.x, movementRaw.z) * Mathf.Rad2Deg + m_playerCam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            m_controller.Move(transform.forward * (m_speed * Time.deltaTime));

        }
        else if (!m_isGrounded && movementSmooth.magnitude > 0.1f && !isGliding) //If in the air and moving
        {
            float targetAngle = Mathf.Atan2(movementSmooth.x, movementSmooth.z) * Mathf.Rad2Deg + m_playerCam.eulerAngles.y;

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            m_controller.Move(transform.forward * (movementAir * Time.deltaTime));

        }
        else if (!m_isGrounded && !isGliding)//If in the air and not moving / gliding
        {
            m_controller.Move(movementSmooth * (movementAir * Time.deltaTime));

        }
        else if (!m_isGrounded && isGliding && movementRaw.magnitude > 0.1f) //if gliding while moving
        {
            //Update smooth for when we release
            movementSmooth = new Vector3(m_smoothX, 0.0f, m_smoothZ);
            float targetAngle = Mathf.Atan2(movementRaw.x, movementRaw.z) * Mathf.Rad2Deg + m_playerCam.eulerAngles.y;

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            m_controller.Move(transform.forward * (m_baseSpeed * Time.deltaTime));

        }
        else if (!m_isGrounded && isGliding) //If gliding without moving
        { 
            //Update Smooth so we keep the speed of gliding when we let release
            movementSmooth = new Vector3(m_smoothX, 0.0f, m_smoothZ);
            m_controller.Move(movementSmooth * (movementAir * Time.deltaTime));
        }
    }
    void ApplyGravity()
    {
        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {
            Velocity.y += m_jumpheight;
        }
        else if (!m_isGrounded && isGliding)
        {
            //We are not aiming for a exponential fall,
            //but a constant one
            m_freeLook.m_XAxis.m_MaxSpeed = 450;
            Velocity.y += (m_gravity / 4) * Time.deltaTime;
        }
        else if (!m_isGrounded)
        {
            Velocity.y += m_gravity * Time.deltaTime;
            m_freeLook.m_XAxis.m_MaxSpeed = 0;

        }

        m_controller.Move(Velocity * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(Time.time);
    }
}
