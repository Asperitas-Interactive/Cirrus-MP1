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
    //The Destination it checks
    private Vector3 m_destinationMax;
    private Vector3 m_destinationMin;

    public GameObject m_Player;
    public BoxCollider m_Trigger;

    // Start is called before the first frame update
    void Start()
    {
        m_destinationMax = transform.position + m_displacementPos;
        m_destinationMin = transform.position + m_displacementNeg;
    }


    // Update is called once per frame
    void Update()
    {
        //Move towards location
        transform.Translate(m_vectorSpeed * Time.deltaTime, Space.World);
        //Clamp between for correct calculation
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, m_destinationMin.x, m_destinationMax.x),
            Mathf.Clamp(transform.position.y, m_destinationMin.y, m_destinationMax.y),
            Mathf.Clamp(transform.position.z, m_destinationMin.z, m_destinationMax.z));
        //check if reached
        DestinationReach();
    }

    //Check if it reached its destination
    void DestinationReach()
    {
        if(transform.position.x == m_destinationMax.x && transform.position.y == m_destinationMax.y && transform.position.z == m_destinationMax.z)
        {
            m_vectorSpeed = -m_vectorSpeed;
        }

        if (transform.position.x == m_destinationMin.x && transform.position.y == m_destinationMin.y && transform.position.z == m_destinationMin.z)
        {
            m_vectorSpeed = -m_vectorSpeed;
        }
    }

    private void LateUpdate()
    {
        if (m_Trigger.bounds.Contains(m_Player.transform.position))
        {
            m_Player.GetComponent<CharacterController>().Move(m_vectorSpeed * Time.deltaTime);
        }
    }

}
