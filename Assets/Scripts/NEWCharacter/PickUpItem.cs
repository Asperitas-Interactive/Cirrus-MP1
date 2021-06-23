using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private GameObject m_pickUp;

    bool m_canPickUp = false;
    
    
    void Update()
    {
        if (Input.GetButtonUp("PickUp") && m_pickUp != null)
        {
            m_pickUp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            m_pickUp.transform.rotation = Quaternion.identity;
            m_pickUp.GetComponent<MovePlat>().ResetPosition();
            m_pickUp = null;
        }

        if(m_pickUp != null)
        {
            m_pickUp.transform.Rotate(new Vector3(10 * Time.deltaTime, 0.0f, 0.0f));
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PickUp")
        {
            m_canPickUp = true;
            if (Input.GetButton("PickUp"))
            {
                if (m_pickUp == null)
                {
                    m_pickUp = other.gameObject;
                    m_pickUp.GetComponent<MovePlat>().enabled = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickUp")
        { 
            //m_pickUp.GetComponent<MovePlat>().enabled = true;
        //     
            //Invoke("other.gameObject.GetComponent<MovePlat>().ResetPosition()", 3);

        }
    }

    private void LateUpdate()
    {
        if(m_pickUp != null)
        {
            m_pickUp.transform.position = transform.position + (transform.forward * 2f) + (transform.up);
        }
    }
    
}
