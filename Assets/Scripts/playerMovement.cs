using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

public class playerMovement : MonoBehaviour
{

    float glideTimer = 0.0f;
    float smoothTime = 0.1f;
    float smoothVel;

    public bool glide;

    //Movement Based Variables
    public float speed = 12.0f;
    public float jumpHeight;


    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Transform cam;

    [Range(0f, 20.0f)]
    public float glideFactor = -1.8f;

    Rigidbody rb;

    bool isJumping;
    bool isGrounded;
    float defaultPos;
    bool canGlide;
    bool isGliding;
    //Interaction Based Variables

    Vector3 dir;
    Vector3 move;


    float jumpTimer;
    // Start is called before the first frame update
    void Start()
    {
        //controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpTimer -= Time.deltaTime;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.GetChild(4).GetComponent<Animator>().SetFloat("inputX", x);
        transform.GetChild(4).GetComponent<Animator>().SetFloat("inputY", y);






        //if (!isGrounded)
        //{
        //    x = Input.GetAxis("Horizontal");
        //    y = Input.GetAxis("Vertical");
        //}

        //Get input axes


        //Move with local dir

        move = new Vector3(x, 0f, y).normalized;


        float angle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        float smAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref smoothVel, smoothTime);

        if (isGrounded)
        {
            canGlide = false;
            isGliding = false;
            // yield return new WaitForSeconds(0.5f);
            if (jumpTimer < 0.0f)
            {
                isJumping = false;
                transform.GetChild(4).GetComponent<Animator>().SetBool("isJumping", false);
                transform.GetChild(4).GetComponent<Animator>().SetBool("isGliding", false);

            }
        }

        transform.GetChild(4).GetComponent<Animator>().SetFloat("speed", rb.velocity.magnitude);

        transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);

        if (move.magnitude > 0.1f)
        {


            dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            if (transform.parent == null)
            {
                rb.velocity = new Vector3((dir * speed).x, rb.velocity.y, (dir * speed).z);
            }
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            rb.angularVelocity = Vector3.zero;
        }




        //Fall down

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
           transform.GetChild(4).GetComponent<Animator>().SetBool("isJumping", true);

            isJumping = true;
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            defaultPos = transform.position.y;
            jumpTimer = 0.2f;
        }

        if (isGrounded && !isJumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }


        if (Input.GetButtonUp("Jump"))
        {
            canGlide = true;
            glideTimer = glideFactor;
        }

        if (Input.GetButton("Jump") && canGlide)
        {
            RaycastHit hit = new RaycastHit();
            transform.GetChild(4).GetComponent<Animator>().SetBool("isGliding", true);

            glideTimer -= Time.deltaTime;

            float distToGround = 5f;
            if (!isGliding)
            {
                if (Physics.Raycast(transform.position, -Vector3.up, out hit))
                {
                    //Debug.Log(hit.distance);
                    distToGround = hit.distance;
                }

                rb.velocity = new Vector3(rb.velocity.x, -0.5f, rb.velocity.z);

                isGliding = true;
            }
            float currDist;

            Physics.Raycast(transform.position, -Vector3.up, out hit);
            currDist = hit.distance;

            if (glideTimer > 0.0f)
            {

            }
            else
            {
                rb.AddForce(Vector3.up * distToGround * -3 * Time.deltaTime, ForceMode.VelocityChange);
            }

            if (rb.velocity.y < -1.0f)
                rb.AddForce(Vector3.up * distToGround * 10f * Time.deltaTime, ForceMode.VelocityChange);
            else
                rb.AddForce(Vector3.up * 1f * Time.deltaTime, ForceMode.VelocityChange);

        }
        else
        {
            transform.GetChild(4).GetComponent<Animator>().SetBool("isGliding", false);

        }


    }

    void FixedUpdate()
    {
        if (transform.parent != null)
        {
            if (move.magnitude > 0.1f)
            {
                rb.velocity = new Vector3((dir * speed).x, rb.velocity.y, (dir * speed).z);

            }

            else
            {
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
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