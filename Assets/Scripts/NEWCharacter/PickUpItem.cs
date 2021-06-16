using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private GameObject m_pickUp;

    bool m_canPickUp = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("PickUp") && m_pickUp != null)
        {
            m_pickUp.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
                }
            }
        }
    }

    private void LateUpdate()
    {
        if(m_pickUp != null)
        {
            m_pickUp.transform.Rotate(transform.position, 10 * Time.deltaTime);
        }
    }
}
