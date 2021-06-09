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
    private bool m_isGrounded;
    [SerializeField]
    private float m_gravity = 20.0f;

    //Axis
    [SerializeField]
    private float m_VelX;
    [SerializeField]
    private float m_VelZ;


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

        m_controller.Move(movement * m_speed * Time.deltaTime);
    }
}
