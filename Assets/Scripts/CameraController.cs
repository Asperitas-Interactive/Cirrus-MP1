using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class CameraController : MonoBehaviour
{

    [SerializeField] private CinemachineFreeLook m_freeLook;

    [Header("Cutscene 1")]
    [SerializeField] private GameObject m_collectibleCam;
    [SerializeField] private GameObject m_recieverCam;

    [Header("Cutscene 2")]
    [SerializeField] private GameObject m_doorCam;
    [SerializeField] private GameObject m_recieverNearCam;
    [SerializeField] private Transform m_depositLocation;
    [SerializeField] private Animator m_pickup;
    
    [SerializeField] private playerMovement m_playerMovement;
    


    private IEnumerator eCutscene1()
    {
        m_collectibleCam.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        m_collectibleCam.SetActive(false);
        m_recieverCam.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        m_recieverCam.SetActive(false);
        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = false;
    }


    public void Cutscene1()
    {
        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = true;
        else
        {
            Debug.Log("Player not assigned in script CameraController.cs");
        }

        StartCoroutine(eCutscene1());

    }

    public void Cutscene2()
    {
        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = true;
        else
        {
            Debug.Log("Player not assigned in script CameraController.cs");
        }

        StartCoroutine(eCutscene2());
    }

    bool hasPath()
    {
        var agent = m_playerMovement.gameObject.GetComponent<NavMeshAgent>();

        if (agent.hasPath)
            return false;
        else return true;
    }

    private IEnumerator eCutscene2()
    {
        m_recieverNearCam.SetActive(true);
        var agent = m_playerMovement.gameObject.GetComponent<NavMeshAgent>();
        yield return new WaitForSeconds(2f);
        agent.enabled = true;
        agent.SetDestination(m_depositLocation.position);
        m_pickup.tag = "Untagged";

      //  m_pickup.gameObject.transform.position = agent.transform.position + Vector3.forward;
        yield return new WaitForSeconds(4f);
        m_playerMovement.gameObject.GetComponent<PickUpItem>().DisablePickup();
        
        agent.transform.rotation = m_depositLocation.rotation;
        m_pickup.SetTrigger("Execute");
        yield return new WaitForSeconds(3f);
        m_recieverNearCam.SetActive(false);
        agent.enabled = false;
        m_doorCam.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        m_doorCam.SetActive(false);

        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = false;

    }
    private void enable()
    {
        m_freeLook.m_YAxisRecentering.m_enabled = true;
    }
    
    public void SetfreeCam()
    {
        m_freeLook.m_YAxis.Value = 0f;
        m_freeLook.m_YAxisRecentering.m_enabled = false;
        Invoke("enable", 2);
    }
    
}
