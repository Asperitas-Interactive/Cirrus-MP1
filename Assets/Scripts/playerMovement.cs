using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngineInternal;

public class playerMovement : MonoBehaviour
{

    float m_glideTimer = 0.0f;
    float m_smoothTime = 0.1f;
    float m_smoothVel;

    [FormerlySerializedAs("glide")] public bool m_glide;

    //Movement Based Variables
    [FormerlySerializedAs("speed")] public float m_speed = 12.0f;
    [FormerlySerializedAs("jumpHeight")] public float m_jumpHeight;


    Vector3 m_velocity;

    [FormerlySerializedAs("groundCheck")] public Transform m_groundCheck;
    [FormerlySerializedAs("groundDistance")] public float m_groundDistance = 0.4f;
    [FormerlySerializedAs("groundMask")] public LayerMask m_groundMask;

    private Transform m_cam;

    [FormerlySerializedAs("glideFactor")] [Range(0f, 20.0f)]
    public float m_glideFactor = -1.8f;

    Rigidbody m_rb;

    bool m_isJumping;
    bool m_isGrounded;
    float m_defaultPos;
    bool m_canGlide;
    bool m_isGliding;
    //Interaction Based Variables

    Vector3 m_dir;
    Vector3 m_move;


    float m_jumpTimer;
    // Start is called before the first frame update
    void Start()
    {
        //controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        m_cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_jumpTimer -= Time.deltaTime;
        m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundMask);

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // transform.GetChild(4).GetComponent<Animator>().SetFloat("inputX", x);
        // transform.GetChild(4).GetComponent<Animator>().SetFloat("inputY", y);






        //if (!isGrounded)
        //{
        //    x = Input.GetAxis("Horizontal");
        //    y = Input.GetAxis("Vertical");
        //}

        //Get input axes


        //Move with local dir

        m_move = new Vector3(x, 0f, y).normalized;


        float angle = Mathf.Atan2(m_move.x, m_move.z) * Mathf.Rad2Deg + m_cam.eulerAngles.y;

        float smAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref m_smoothVel, m_smoothTime);

        if (m_isGrounded)
        {
            m_canGlide = false;
            m_isGliding = false;
            // yield return new WaitForSeconds(0.5f);
            if (m_jumpTimer < 0.0f)
            {
                m_isJumping = false;
                // transform.GetChild(4).GetComponent<Animator>().SetBool("isJumping", false);
                // transform.GetChild(4).GetComponent<Animator>().SetBool("isGliding", false);

            }
        }

        transform.GetChild(2).GetComponent<Animator>().SetFloat("Speed", m_rb.velocity.magnitude);

        transform.rotation = Quaternion.Euler(0f, m_cam.eulerAngles.y, 0f);

        if (m_move.magnitude > 0.1f)
        {


            m_dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            if (transform.parent == null)
            {
                m_rb.velocity = new Vector3((m_dir * m_speed).x, m_rb.velocity.y, (m_dir * m_speed).z);
            }
        }
        else
        {
            m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
            m_rb.angularVelocity = Vector3.zero;
        }




        //Fall down

        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {
           //transform.GetChild(2).GetComponent<Animator>().SetBool("isJumping", true);

            m_isJumping = true;
            m_rb.AddForce(Vector3.up * Mathf.Sqrt(m_jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            m_defaultPos = transform.position.y;
            m_jumpTimer = 0.2f;
        }

        if (m_isGrounded && !m_isJumping)
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, 0f, m_rb.velocity.z);
        }


        if (Input.GetButtonUp("Jump"))
        {
            m_canGlide = true;
            m_glideTimer = m_glideFactor;
        }

        if (Input.GetButton("Jump") && m_canGlide)
        {
            RaycastHit hit = new RaycastHit();
            //transform.GetChild(4).GetComponent<Animator>().SetBool("isGliding", true);

            m_glideTimer -= Time.deltaTime;

            float distToGround = 5f;
            if (!m_isGliding)
            {
                if (Physics.Raycast(transform.position, -Vector3.up, out hit))
                {
                    //Debug.Log(hit.distance);
                    distToGround = hit.distance;
                }

                m_rb.velocity = new Vector3(m_rb.velocity.x, -0.5f, m_rb.velocity.z);

                m_isGliding = true;
            }
            float currDist;

            Physics.Raycast(transform.position, -Vector3.up, out hit);
            currDist = hit.distance;

            if (m_glideTimer > 0.0f)
            {

            }
            else
            {
                m_rb.AddForce(Vector3.up * distToGround * -3 * Time.deltaTime, ForceMode.VelocityChange);
            }

            if (m_rb.velocity.y < -1.0f)
                m_rb.AddForce(Vector3.up * distToGround * 10f * Time.deltaTime, ForceMode.VelocityChange);
            else
                m_rb.AddForce(Vector3.up * 1f * Time.deltaTime, ForceMode.VelocityChange);

        }
        else
        {
            //transform.GetChild(4).GetComponent<Animator>().SetBool("isGliding", false);

        }


    }

    void FixedUpdate()
    {
        if (transform.parent != null)
        {
            if (m_move.magnitude > 0.1f)
            {
                m_rb.velocity = new Vector3((m_dir * m_speed).x, m_rb.velocity.y, (m_dir * m_speed).z);
                float targetAngle = Mathf.Atan2(m_rb.velocity.x, m_rb.velocity.z) * Mathf.Rad2Deg + Camera.main.gameObject.transform.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            }

            else
            {
                m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        //4 is water
        if (other.gameObject.layer == 4)
        {
            rb.AddForce(0.0f, -1.0f * Physics.gravity.y, 0.0f, ForceMode.VelocityChange);
        }

    }*/
};