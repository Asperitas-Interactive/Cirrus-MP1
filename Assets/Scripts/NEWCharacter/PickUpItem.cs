using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickUpItem : MonoBehaviour
{
    private GameObject m_pickUp;

    bool m_canPickUp = false;
    private bool m_pickepUp = false;
    private NavMeshAgent m_agent;

    private void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    public void DisablePickup()
    {
        if (m_pickUp != null)
        {
            m_pickepUp = false;
            m_pickUp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            m_pickUp.transform.rotation = Quaternion.identity;
            m_pickUp.GetComponent<MovePlat>().ResetPosition();
            m_canPickUp = false;
            m_pickUp = null;
        }
    }

    public void DisablePickupNoRes()
    {
        m_pickepUp = false;
        m_pickUp.GetComponent<Rigidbody>().velocity = Vector3.zero;
        m_pickUp.transform.rotation = Quaternion.identity;
        //m_pickUp.GetComponent<MovePlat>().ResetPosition();
        m_canPickUp = false;
        m_pickUp = null;
    }
    
    void Update()
    {

        if (GetComponent<playerMovement>().m_cutscenePlayin) return;

        if (Input.GetButtonDown("PickUp") && m_pickUp != null && m_pickepUp)
        {
           

            DisablePickup();
            

        }

        else if (Input.GetButtonDown("PickUp") && m_canPickUp == true && m_pickepUp ==false)
        {
            void picked()
            {
                m_pickepUp = true;
            }
                m_pickUp.GetComponent<MovePlat>().enabled = false;
                m_canPickUp = false;
                picked();
        }
        if(m_pickUp != null)
        {
            m_pickUp.transform.Rotate(new Vector3(10 * Time.deltaTime, 0.0f, 0.0f));
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp")
        {
            m_pickUp = other.gameObject;
            m_canPickUp = true;
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickUp")
        {
            m_canPickUp = false;
            //m_pickUp.GetComponent<MovePlat>().enabled = true;
            //     
            //Invoke("other.gameObject.GetComponent<MovePlat>().ResetPosition()", 3);

        }
    }

    private void LateUpdate()
    {
        if(m_pickUp != null && m_pickepUp == true)
        {
            m_pickUp.transform.position = transform.position + (transform.forward * 2f) + (transform.up);
        }
        if(m_agent.enabled && m_pickUp != null)
        {
            m_pickUp.transform.position = transform.position + (transform.forward);
        }
    }
    
}
