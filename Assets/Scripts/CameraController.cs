using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
 [SerializeField] private GameObject m_collectibleCam;
 [SerializeField] private GameObject m_doorCam;
 [SerializeField] private GameObject m_recieverCam;
 [SerializeField] private playerMovement m_playerMovement;
 
private IEnumerator  eCutscene1()
{
    m_collectibleCam.SetActive(true);
    yield return new WaitForSeconds(4.5f);
    m_collectibleCam.SetActive(false);
    m_recieverCam.SetActive(true);
    yield return new WaitForSeconds(4.5f);
    m_recieverCam.SetActive(false);
    if(m_playerMovement != null)
        m_playerMovement.m_cutscenePlayin = false;
}

public void Cutscene1()
{
    if(m_playerMovement != null)
        m_playerMovement.m_cutscenePlayin = true;
    else
    {
        Debug.Log("Player not assigned in script CameraController.cs");
    }
    
    StartCoroutine(eCutscene1());

}
public void Cutscene2()
{
    if(m_playerMovement != null)
        m_playerMovement.m_cutscenePlayin = true;
    else
    {
        Debug.Log("Player not assigned in script CameraController.cs");
    }
    StartCoroutine(eCutscene2());
}
private IEnumerator eCutscene2()
{
    m_doorCam.SetActive(true);
    yield return new WaitForSeconds(4.0f);
    m_doorCam.SetActive(false);
    
    if(m_playerMovement != null)
        m_playerMovement.m_cutscenePlayin = false;
    
}


}
