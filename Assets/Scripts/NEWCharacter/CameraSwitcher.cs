using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    enum eState
    {
        normal, 
        aim,
    }

    eState m_state;
    [SerializeField] private GameObject m_aimCamera;
    [SerializeField] private GameObject m_normalCamera;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Aim"))
        {
            if (m_state == eState.aim)
                m_state = eState.normal;
            else
                m_state = eState.aim;
        
        }


        if (m_state == eState.aim && !m_aimCamera.activeSelf)
        {
            m_aimCamera.SetActive(true);
            m_normalCamera.SetActive(false);
        }
        else if(m_state == eState.normal && !m_normalCamera.activeSelf)
        {
            m_aimCamera.SetActive(false);
            m_normalCamera.SetActive(true);            
        }
    }


    public void Aim(bool _aim)
    {
        if (_aim)
        {
            m_aimCamera.SetActive(true);
            m_normalCamera.SetActive(false);
            m_state = eState.aim;
        }
        else
        {
            m_state = eState.normal;
            m_aimCamera.SetActive(false);
            m_normalCamera.SetActive(true);             
        }
    }
}
