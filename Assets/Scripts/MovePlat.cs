using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class MovePlat : MonoBehaviour
{
    //The Distance the end points are
    [FormerlySerializedAs("DisplacementPos")] public Vector3 m_displacementPos;
    [FormerlySerializedAs("DisplacementNeg")] public Vector3 m_displacementNeg;
    //The Speed you arrive at a end point
    public Vector3 m_vectorSpeed;

    private Vector3 m_origin;
    //The Destination it checks
    private Vector3 m_destinationMax;
    private Vector3 m_destinationMin;

    private Rigidbody m_rigidBody;

    //Prevents it from getting stuck
    bool reachmax = false;
    bool reachmin = false;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_origin = transform.position;
        m_destinationMax = transform.position + m_displacementPos;
        m_destinationMin = transform.position + m_displacementNeg;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Move towards location
        m_rigidBody.MovePosition(transform.position + m_vectorSpeed * Time.fixedDeltaTime);
        //Clamp between for correct calculation
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, m_destinationMin.x, m_destinationMax.x),
            Mathf.Clamp(transform.position.y, m_destinationMin.y, m_destinationMax.y),
            Mathf.Clamp(transform.position.z, m_destinationMin.z, m_destinationMax.z));
        //check if reached
        DestinationReach();
    }

    public void ResetPosition()
    {

        Invoke("resetPos", 3);
      
    }

    private void resetPos()
    {
        transform.position = m_origin;
    }
    //Check if it reached its destination
    void DestinationReach()
    {
        //Without the bool itd get stuck at a destination and have its speed reset over and over

        if(transform.position.x == m_destinationMax.x && transform.position.y == m_destinationMax.y && transform.position.z == m_destinationMax.z && !reachmax)
        {
            m_vectorSpeed = -m_vectorSpeed;
            reachmax = true;
            reachmin = false;
        }

        if (transform.position.x == m_destinationMin.x && transform.position.y == m_destinationMin.y && transform.position.z == m_destinationMin.z && !reachmin)
        {
            m_vectorSpeed = -m_vectorSpeed;
            reachmin = true;
            reachmax = false;
        }
    }
}
